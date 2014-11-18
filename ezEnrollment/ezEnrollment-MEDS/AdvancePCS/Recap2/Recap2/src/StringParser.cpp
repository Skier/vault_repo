// StringParser.cpp: implementation of the CStringParser class.
//
//////////////////////////////////////////////////////////////////////

#include "client/stdafx.h"
#include "client/StringParser.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CStringParser::CStringParser()
{
	m_pAlloc		= NULL;
	m_argv			= NULL;
	m_argc			= 0;
}

CStringParser::~CStringParser()
{
	Empty();
}

void CStringParser::Empty()
{
	if ( m_pAlloc )
	{
		free( m_pAlloc );
		m_pAlloc		= NULL;
		m_argv			= NULL;
		m_argc			= 0;
	}
}

int CStringParser::Parse( LPCTSTR pszCmd, const CParseOptions& po )
{
	Empty();

	if ( pszCmd )
	{
		int         numargs;
		int         numchars;
		TCHAR*		pBuf;

		Parse( pszCmd, po, numargs, numchars );

		#ifndef NDEBUG 
		assert( numargs > 0 );
		assert( numchars >= 0 );
		_tprintf( _T("Count of args needed = %d\n"), numargs );
		_tprintf( _T("Count of chars needed = %d\n"), numchars );
		#endif // #ifndef NDEBUG

		m_pAlloc = (BYTE*)malloc( sizeof(TCHAR) * numchars + sizeof(TCHAR*) * numargs );
		if (!m_pAlloc) {
			::SetLastError(ERROR_OUTOFMEMORY);
			return( 0 );
		}

		m_argv	= (TCHAR**)m_pAlloc;
		pBuf	= (TCHAR*)( m_pAlloc  + ( sizeof(TCHAR*) * numargs ) );
		Parse( pszCmd, po, numargs, numchars, m_argv, pBuf );
		m_argc	= numargs - 1;

		#ifndef NDEBUG 
		assert( numargs > 0 );
		_tprintf( _T("Count of args returned = %d\n"), numargs );
		_tprintf( _T("Count of chars returned = %d\n"), numchars );
		#endif // #ifndef NDEBUG
	}
    return( m_argc );
}

void CStringParser::Parse( LPCTSTR pszStr, const CParseOptions& po, int& numargs, int& numchars, TCHAR** argv, TCHAR* args )
{
    numchars	= 0;
    numargs		= 1;

	assert( (argv == NULL && args == NULL) || (argv != NULL && args != NULL) );

    if ( NULL == pszStr || *pszStr == _T('\0') ) 
	{
        if ( argv ) argv[0] = NULL;
        return;
    }

    LPCTSTR         p			= pszStr;
    bool            bInQuote	= false;    /* true = inside quotes */
    bool            bCopyChar	= true;     /* true = copy char to *args */
	bool			bNeedArg	= false;	/* true = found delimiter and not gathering */
    unsigned        numslash	= 0;		/* num of backslashes seen */

	assert( *p != _T('\0') );

	if ( po.IsQuoter( *p ) && p[1] != _T('\0') )
	{
		bInQuote = true;
		p++;
	}

    /* loop on each argument */
    for (;;) 
	{
        if (*p == _T('\0'))
		{
			if ( bNeedArg && !po.IsRTrim() )
			{
				/* scan an empty argument */
				if (argv)
					*argv++ = args;     /* store ptr to arg */
				++numargs;
				/* add an empty argument */
				if (args)
					*args++ = _T('\0');	/* terminate string */
				++numchars;
			}
			break;              /* end of args */
		}

        /* scan an argument */
        if (argv)
            *argv++ = args;     /* store ptr to arg */
        ++numargs;

        /* loop through scanning one argument */
        for (;;) 
		{     
			bCopyChar = true;
			numslash = 0;

			if ( po.IsEscape( *p )  )
			{
				/* 
				** Rules:	2N backslashes + " ==> N backslashes and begin/end quote
				**			2N+1 backslashes + " ==> N backslashes + literal "
                **			N backslashes ==> N backslashes 
				*/				
				/* count number of backslashes for use below */
				do { ++p; ++numslash; } while (*p == po.GetEscape());
			}
			
			if ( po.IsQuoter( *p ) ) 
			{
				/*
				** if 2N backslashes before, start/end quote, otherwise
				** copy literally 
				*/
				if (numslash % 2 == 0) 
				{
					if (bInQuote) 
					{
						if (p[1] == po.GetQuoter())
							p++;    /* Double quote inside quoted string */
						else        /* skip first quote char and copy second */
							bCopyChar = po.IsKeepQuote();
					} else
						bCopyChar = po.IsKeepQuote();       /* don't copy quote */
					bInQuote = !bInQuote;
				}
				numslash /= 2;          /* divide numslash by two */
			}

			/* copy slashes */
			while (numslash--) 
			{
				if (args)
					*args++ = po.GetEscape();
				++numchars;
			}

            /* if at end of arg, break loop */
            if ( *p == _T('\0') )
			{
                break;
			}

			if ( !bInQuote && *p == po.GetDelimiter() )
			{
				do
				{
					p++;
				} while ( po.IsGather() && *p == po.GetDelimiter() );
				bNeedArg = true;
				break;
			}

            /* copy character into argument */
            if (bCopyChar) 
			{
                if (args)
                    *args++ = *p;
                ++numchars;
            }
            ++p;

			bNeedArg = false;
        }

        /* null-terminate the argument */
        if (args)
            *args++ = _T('\0');          /* terminate string */
        ++numchars;
    }

    /* We put one last argument in -- a null ptr */
    if (argv)
        *argv++ = NULL;
}

#ifndef NDEBUG 
void CStringParser::Dump() const
{
	_tprintf( _T("Count of args = %d\n"), GetCount() );
	for ( int i = 0; i < GetCount(); i++ )
		_tprintf( _T("Arg[%d] = <%s>\n"), i, GetAt(i) );
}
#endif // #ifndef NDEBUG
