/*
 *  $RCSfile: XmlLoader.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// XmlLoader.cpp: implementation of the CXmlLoader class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/XmlLoader.h>
#include <atf/XmlNode.h>
#include <atf/XmlLoadException.h>
#include <memory.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////


CString CharToString(const char *s);
void StartElementHnd(void *userData, const char *name, const char **atts);
void EndElementHnd(void *userData, const char* name);
void TextHnd(void *userData, const char *s, int len);
void CommentHnd(void *userData, const char *data);

CXmlLoader::CXmlLoader(IXmlIOHandler & handler):m_handler(handler){
    m_parser = XML_ParserCreate(NULL);
    m_ctx.root = m_ctx.node = NULL;
    XML_SetUserData(m_parser, (void*)&m_ctx);
    XML_SetElementHandler(m_parser, StartElementHnd, EndElementHnd);
    XML_SetCharacterDataHandler(m_parser, TextHnd);
    XML_SetCommentHandler(m_parser, CommentHnd);
};

CXmlLoader::~CXmlLoader() {
    XML_ParserFree(m_parser);
};


CXmlDocument& CXmlLoader::Load(CXmlDocument & doc){
    bool done;
    m_ctx.root = m_ctx.node = NULL;

    do {
        size_t len = GetIOHandler().LoadBuffer(m_buf, BUFSIZE);
        done = (len < BUFSIZE);
        if (!XML_Parse(m_parser, m_buf, len, done)) {
            CXmlLoadException ex(XML_ErrorString(XML_GetErrorCode(m_parser)), 
                              XML_GetErrorCode(m_parser), 
                              XML_GetCurrentLineNumber(m_parser));
            throw ex;
        }
    } while (!done);

    doc.SetRoot(m_ctx.root);
    return doc;
};


inline static CString CharToString(const char *s) {
    return CString(s);
}


static void StartElementHnd(void *userData, const char *name, const char **atts) {

    XmlParsingContext *ctx = (XmlParsingContext*)userData;
    CXmlNode *node = new CXmlNode(XML_ELEMENT_NODE, CharToString(name));
    const char **a = atts;
    while (*a) {
        node->AddProperty(CharToString(a[0]), CharToString(a[1]));
        a += 2;
    }
    if (ctx->root == NULL)
        ctx->root = node;
    else
        ctx->node->AddChild(node);
    ctx->node = node;
    ctx->lastAsText = NULL;
}

static void EndElementHnd(void *userData, const char* name) {
    XmlParsingContext *ctx = (XmlParsingContext*)userData;

    ctx->node = ctx->node->GetParent();
    ctx->lastAsText = NULL;
}

static void TextHnd(void *userData, const char *s, int len) {
    XmlParsingContext *ctx = (XmlParsingContext*)userData;
    char *buf = new char[len + 1];

    buf[len] = '\0';
    memcpy(buf, s, (size_t)len);
    CString value = ctx->node->GetContent();
    value += buf;
    ctx->node->SetContent(value);
/*
    if (ctx->lastAsText) {
        CString str(ctx->lastAsText->GetContent());
        str += CharToString(buf);
        ctx->lastAsText->SetContent(str);
    } else {
        bool whiteOnly = true;
        for (char *c = buf; *c != '\0'; c++)
            if (*c != ' ' && *c != '\t' && *c != '\n' && *c != '\r') {
                whiteOnly = false;
                break;
            }
        if (!whiteOnly) {
            ctx->lastAsText = new CXmlNode(XML_TEXT_NODE, "text",
                                            CharToString(buf));
            ctx->node->AddChild(ctx->lastAsText);
        }
    }
*/
    delete[] buf;
}

static void CommentHnd(void *userData, const char *data)
{
    XmlParsingContext *ctx = (XmlParsingContext*)userData;
#if 0

    if (ctx->node) {
        ctx->node->AddChild(new CXmlNode(XML_COMMENT_NODE,
                            "comment", CharToString(data)));
    }
#endif 
    ctx->lastAsText = NULL;
}

