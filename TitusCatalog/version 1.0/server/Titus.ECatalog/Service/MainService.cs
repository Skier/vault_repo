using System;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using Titus.ECatalog.Entity;
using Titus.ECatalog.Data;
using Titus.ECatalog.Util;

namespace Titus.ECatalog.Service
{

    public class MainService
    {

        public List<PageDataObject> Search(DocumentDataObject documentInfo, string expression,
                bool entireWords, bool specifiedSequence, bool caseSensitive)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    List<PageDataObject> result = new List<PageDataObject>();

                    string[] tokensToSearch = expression.Split(new char[] {' ', ','});
                    List<string> wordsToSearch = new List<string>();
                    foreach (string word in tokensToSearch)
                    {
                        if (0 < word.Length)
                        {
                            wordsToSearch.Add((caseSensitive)? word: word.ToUpper());
                        }
                    }

                    foreach (PageDataObject pageInfo in documentInfo.Pages)
                    {
                        pageInfo.Tokens = Token.GetInstance().FindByPageId(tran, pageInfo.DocumentPageId);

                        if (0 == pageInfo.Tokens.Count)
                        {
                            continue;
                        }

                        PageDataObject foundPage = new PageDataObject();

                        foreach (string word in wordsToSearch)
                        {
                            foreach (TokenDataObject token in pageInfo.Tokens)
                            {
                                if (caseSensitive)
                                {
                                    if (token.Text.Contains(word))
                                    {
                                        foundPage.Tokens.Add(token);
                                    }
                                }
                                else
                                {
                                    if (token.Text.ToUpper().Contains(word))
                                    {
                                        foundPage.Tokens.Add(token);
                                    }
                                }
                            }
                        }

                        if (0 < foundPage.Tokens.Count)
                        {
                            foundPage.PageNumber = pageInfo.PageNumber;

                            result.Add(foundPage);
                        }
                    }

                    tran.Commit();

                    return result;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception)
                    {
                    }

                    throw ex;
                }
            }
        }

        public DocumentDataObject PreloadDocument(int documentId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    DocumentDataObject documentInfo = Document.GetInstance().FindById(tran, documentId);
                    documentInfo.Pages = Page.GetInstance().FindByDocumentId(tran, documentId);

                    foreach (PageDataObject pageInfo in documentInfo.Pages)
                    {
                        pageInfo.Notes = Note.GetInstance().FindByPageId(tran, pageInfo.DocumentPageId);
                    }

                    tran.Commit();

                    return documentInfo;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception)
                    {
                    }

                    throw ex;
                }
            }
        }

        public int StoreNote(NoteDataObject noteInfo)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    if (0 == noteInfo.NoteId)
                    {
                        Note.GetInstance().Insert(tran, noteInfo);
                    }
                    else
                    {
                        Note.GetInstance().Update(tran, noteInfo);
                    }

                    tran.Commit();

                    return noteInfo.NoteId;
                }
                catch (SqlException ex)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception)
                    {
                    }

                    throw ex;
                }
            }
        }

        public void RemoveNote(int noteId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    Note.GetInstance().Remove(tran, noteId);

                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception)
                    {
                    }

                    throw ex;
                }
            }
        }

    }

}
