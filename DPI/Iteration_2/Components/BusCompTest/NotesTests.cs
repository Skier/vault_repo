using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class NotesTests
    {
        /*		Data		*/
        int id;
        string user = "user";
        int dmdId = 3;
        DateTime date = DateTime.Now;
        string text = "text";
        
        /*		Constructors		*/
        public NotesTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            NotesTests test = new NotesTests();
            
            // UOW Tests
            test.addNotes();
            test.findNotes();
            test.saveNotes();
            test.findAllNoteses();
            
            try
            {
                test.delNotes();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delNotes:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delNotes: " + e.Message);
            }
            
        }
        [Test]
        public void addNotes()
        {
            UOW uow = new UOW();
            uow.Service = "addNotes";
            Notes cls = new Notes(uow);
            
            cls.User = this.user;
            cls.DmdId = this.dmdId;
            cls.Date = this.date;
            cls.Text = this.text;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addNotes - assert";
            cls = Notes.find(uow, this.id);
            Assertion.Assert(cls.DmdId == this.dmdId);
            uow.close();
        }
        [Test]
        public void findNotes()
        {
            UOW uow = new UOW();
            uow.Service = "findNotes";
            
            Notes cls = Notes.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveNotes()
        {
            UOW uow = new UOW();
            uow.Service = "saveNotes";
            Notes cls = Notes.find(uow, this.id);
            
			string update = " - Update";
            cls.User = this.user;
            cls.DmdId = this.dmdId;
            cls.Date = this.date;
            cls.Text += update;
            cls.User = " sav";
            this.user = cls.User;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveNotes - assert";
            
            cls = Notes.find(uow, this.id);
            Assertion.Assert(cls.Text == this.text + update);
            uow.close();
        }
        [Test]
        public void findAllNoteses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllNoteses";
            Notes[] objs = Notes.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delNotes()
        {
            UOW uow = new UOW();
            uow.Service = "delNotes";
            Notes cls = Notes.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delNotes - assert";
            cls = Notes.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
