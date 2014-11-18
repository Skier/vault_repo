/**
 * $id:kiwi_001

 * Copyright(c) 2006 Adobe Systems Incorporated. All Rights Reserved.
 */
package com.tmnc.mail.view.utils
{
    import flash.xml.XMLNode;
    import flash.xml.XMLNodeType;
    
    /**
     *  Utility class for transforming proper HTML to RichTextEditor HTML,
     *  and back.
     **/
    public class RTEHTMLTransformer
    {
        /**
         *  Transforms the given HTML string to RichTextEditor HTML.
         * 
         *  @param content a String containing proper HTML
         * 
         *  @return a String containing RichTextEditor HTML
         **/
        public static function transformToRTEHTML(content:String):String
        {
            var document:XML = new XML(content);
            
            convertFontsToRTEHTML(document);

            return document.toXMLString();
        }
        
        /**
         *  Transforms the given HTML string to proper HTML.
         * 
         *  @param content a String containing RichTextEditor HTML
         * 
         *  @return a String containing proper HTML
         **/
        public static function transformFromRTEHTML(content:String):String
        {
            var document:XML = new XML(content);
            
            /* convert font tags from the RTE version */
            convertFontsFromRTEHTML(document)
            
            /* remove empty tags.  These can cause parsing errors if they
               get into the XHTML */
            removeEmptyTags(document);
            
            return document.toXMLString();
        }
        
        protected static function convertFontsFromRTEHTML(document:XML) : void
        {
            for each (var font:XML in document..XHTMLNS::FONT)
            {
                var sizeStr:String = font.@SIZE;
                if (sizeStr && sizeStr.length > 0)
                {
                    // The RTE's idea of size is very different from HTML
                    // font sizes.  We need to scale this so it looks
                    // somewhat appropriate on the web.
                    var size:int = font.@SIZE;
                    size = size - 8;
                    font.@SIZE = size;
                }
                
                // We don't want any color formatting from the RTE.
                // Otherwise this text may not show up on blogs that
                // use a template with a dark background.
                if (font.@COLOR)
                    delete font.@COLOR;
            }
        }
        
        protected static function convertFontsToRTEHTML(document:XML) : void
        {
            for each (var font:XML in document..XHTMLNS::FONT)
            {
                var sizeStr:String = font.@SIZE;
                if (sizeStr && sizeStr.length > 0)
                {
                    var size:int = font.@SIZE;
                    
                    // scale the font size back to RTE's idea of font size.
                    size += 8;
                    if (size < 8)
                        size = 8;  // minimum of 8pt
                    
                    font.@SIZE = size;
                }
                
                // Set the color back to black if it is not specified.
                // Otherwise, this font will assume the color of the
                // previous block.
                var color:String = font.@COLOR;
                if (color == null || color.length == 0)
                    font.@COLOR = "#000000";
                    
                var face:String = font.@FACE;
                if (face == null || face.length == 0)
                    font.@FACE = "Verdana";
            }
        }
        
        /**
         * Given an XML document, search through the document to find empty tags
         * (XML tags that contain no childen tags or contain no text).  The
         * XML.toString() method converts empty tags into the <empty/> short
         * hand version which is not allowed in XHTML.
         */
        protected static function removeEmptyTags(node:XML) : void
        {
            // Process this node's children (if any)
            for each (var child:XML in node.children())
            {
                // recursively remove empty child nodes
                removeEmptyTags(child);
            }
                
            // After potentially removing all our children, this node may
            // now be empty.
            deleteIfEmpty(node);
        }
        
        /**
         * Given a node in an XML document, delete it from the document if it
         * is empty (does not contain any children).
         * 
         * @return  true if the given node was deleted from the document,
         * false if the node was not deleted.
         */
        protected static function deleteIfEmpty(node:XML) : Boolean
        {
            if (node.children().length() > 0)
                return false;  // this node is not empty
            
            var name:String = node.localName();
            
            if (node.nodeKind() == "text")
            {
                // don't touch text nodes
                return false;
            }
            else
            {
                if (shouldDeleteNode(node))
                {
                    // This node is empty and we should delete it.
                    var parent:XML = node.parent();
                    
                    if (parent == null)
                        return false;  // don't delete the root node.
                        
                    var childIndex:int = node.childIndex();
                    delete parent.children()[childIndex];
                }
                else
                {
                    // We have an empty tag that we don't want to delete.  Typically,
                    // these tags provide formating (like <p>) and we don't want to
                    // collapse them.  So we'll add a non-breaking space as a child
                    // so that they don't get collapsed into the <empty /> tag
                    // shorthand form by the XML.toString() method.
                    var nbsp:XML = new XML("&#160;");
                    node.appendChild(nbsp);
                    
                    return false;
                }
            }
            
            return true;
        }
        
        /**
         * Given a node, should we delete it?
         * 
         * @return true if this node should be deleted,
         * false otherwise.
         */
        private static function shouldDeleteNode(node:XML) : Boolean
        {
            var name:String = node.localName();
            name = name.toLowerCase();
            
            // If a tag does not specify any visual formatting that
            // would affect the flow of the page, we are free to
            // remove it.  Add other tags here as necessary.
            if (name == "u" || name == "b" || name == "font")
                return true;
            else
                return false;
        }
        
        public static const XHTMLNS:Namespace = new Namespace("http://www.w3.org/1999/xhtml");
    }
}