using System;
using System.Collections.Generic;
using TractInc.TrueTract;
using TractInc.TrueTract.Entity;

namespace TractInc.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isTxt = false;
            TrueTractService ttService = new TrueTractService();

            if (args.Length > 1 && args[1] == "txt")
                isTxt = true;
            
            if (args.Length > 0 && args[0] == "docs")
            {
                Console.WriteLine("<documents>");

                Document docService = new Document();
                DocumentInfo doc;

                List<DocumentInfo> documents = docService.GetAllDocuments();

                foreach (DocumentInfo document in documents)
                {
                    doc = docService.GetDocumentReferences(document.DocID);

                    if (isTxt)
                        Console.WriteLine(doc.toSearchString());
                    else
                        Console.WriteLine(doc.toXml());

                    SearchItemInfo searchItem = new SearchItemInfo(2, doc.DocID, doc.toSearchString(), doc.toXml());
                    searchItem.SearchItemId = ttService.GetSearchItemByItem(document).SearchItemId;

                    ttService.SaveSearchItem(searchItem);
                }

                Console.WriteLine("</documents>");
            }
            else
            {
                Console.WriteLine("<projects>");

                Project projectService = new Project();
                ProjectInfo proj;

                List<ProjectInfo> projects = projectService.GetAllProjectsByClient(1);

                foreach (ProjectInfo project in projects)
                {
                    proj = projectService.LoadFullProject(project.ProjectId);

                    if (isTxt)
                        Console.WriteLine(proj.toSearchString());
                    else
                        Console.WriteLine(proj.toXml());

                    SearchItemInfo searchItem = new SearchItemInfo(1, proj.ProjectId, proj.toSearchString(), proj.toXml());
                    searchItem.SearchItemId = ttService.GetSearchItemByItem(project).SearchItemId;
                    
                    ttService.SaveSearchItem(searchItem);
                }

                Console.WriteLine("</projects>");
            }
        }
    }
}
