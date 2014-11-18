// StringParser.h: interface for the CStringParser class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_STRINGPARSER_H__E3708777_30A5_11D5_A483_00105ADBB436__INCLUDED_)
#define AFX_STRINGPARSER_H__E3708777_30A5_11D5_A483_00105ADBB436__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#ifndef assert
#define assert ASSERT
#endif

namespace Char
{
	const TCHAR			NUL			= _T('\0');
	const TCHAR			TAB			= _T('\t');
	const TCHAR			QUOTE		= _T('\"');
	const TCHAR			SPACE		= _T(' ');
	const TCHAR			SEMICOL		= _T(';');
};

class CParseOptions
{
public:
	CParseOptions( TCHAR chDelimiter, 
				   TCHAR chQuoter	= Char::QUOTE, 
				   TCHAR chEscape	= Char::NUL, 
				   bool bGather		= false, 
				   bool bRTrim		= true, 
				   bool bKeepQuote	= false )
		: m_chDelimiter( chDelimiter )
		, m_chQuoter( chQuoter )
		, m_chEscape( chEscape )
		, m_bGather( bGather )
		, m_bRTrim( bRTrim )
		, m_bKeepQuote( bKeepQuote )
	{}

	inline void SetDelimiter( TCHAR chDelimiter )	{ m_chDelimiter = chDelimiter; }
	inline void SetQuoter( TCHAR chQuoter )			{ m_chQuoter = chQuoter; }
	inline void SetEscape( TCHAR chEscape )			{ m_chEscape = chEscape; }

	inline TCHAR GetDelimiter() const				{ return( m_chDelimiter ); }
	inline TCHAR GetQuoter() const					{ return( m_chQuoter ); }
	inline TCHAR GetEscape() const					{ return( m_chEscape ); }

	inline void NoDelimiter()						{ m_chDelimiter = Char::NUL; }
	inline void NoQuoter()							{ m_chQuoter = Char::NUL; }
	inline void NoEscape()							{ m_chEscape = Char::NUL; }

	inline bool IsDelimiter( TCHAR ch ) const		{ return( m_chDelimiter != Char::NUL && m_chDelimiter == ch ); }
	inline bool IsQuoter( TCHAR ch ) const			{ return( m_chQuoter != Char::NUL && m_chQuoter == ch ); }
	inline bool IsEscape( TCHAR ch ) const			{ return( m_chEscape != Char::NUL && m_chEscape == ch ); }

	inline void SetGather( bool bGather )			{ m_bGather = bGather; }
	inline bool IsGather() const					{ return( m_bGather ); }

	inline void SetRTrim( bool bRTrim )				{ m_bRTrim = bRTrim; }
	inline bool IsRTrim() const						{ return( m_bRTrim ); }

	inline void SetKeepQuote( bool bKeepQuote )		{ m_bKeepQuote = bKeepQuote; }
	inline bool IsKeepQuote() const					{ return( m_bKeepQuote ); }

protected:

	TCHAR		m_chDelimiter;	// to separate each string
	TCHAR		m_chQuoter;		// to introduce a quoted string
	TCHAR		m_chEscape;		// to escape to next character
	bool		m_bGather;		// to treat adjacent delimiters as one delimiter
	bool		m_bRTrim;		// to ignore empty trailing argument
	bool		m_bKeepQuote;	// to keep the quote of a quoted argument
};

const CParseOptions poCmdLine		( Char::SPACE, Char::QUOTE, Char::NUL, true );
const CParseOptions poCsvLine		( Char::SEMICOL, Char::QUOTE, Char::NUL, false );
const CParseOptions poTabbedCsvLine	( Char::TAB, Char::QUOTE, Char::NUL, false );

class CStringParser  
{
public:
	CStringParser();
	virtual ~CStringParser();

	void Empty();
	int Parse( LPCTSTR pszStr, const CParseOptions& po );
	int GetCount() const;
	LPCTSTR GetAt(int nIndex) const;

	#ifndef NDEBUG 
	void Dump() const;
	#endif // #ifndef NDEBUG

protected:

	BYTE*		m_pAlloc;
	TCHAR**		m_argv;
	int			m_argc;

	void Parse( LPCTSTR pszStr, const CParseOptions& po, int& numargs, int& numchars, TCHAR** argv = NULL, TCHAR* args = NULL );

};

inline int CStringParser::GetCount() const
{
	return( m_argc );
}

inline LPCTSTR CStringParser::GetAt(int nIndex) const
{
	assert( m_argv != NULL && nIndex >= 0 && nIndex < m_argc );
	return( m_argv[nIndex] );
}

#endif // !defined(AFX_STRINGPARSER_H__E3708777_30A5_11D5_A483_00105ADBB436__INCLUDED_)
