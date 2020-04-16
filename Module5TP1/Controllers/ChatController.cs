using Module5TP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TP1.Controllers
{

    public class ChatController : Controller
    {
        static List<Chat> listChats = Chat.GetMeuteDeChats();

        // GET: Chat
        public ActionResult Index()
        {
            
            return View(listChats);
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            Chat chatById = listChats.SingleOrDefault(c => c.Id == id);
            if (chatById != null)
            {
                return View(chatById);
            }
            return RedirectToAction("Index");
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            Chat chatById = listChats.SingleOrDefault(c => c.Id == id);
            if (chatById != null)
            {
                return View(chatById);
            }
            return RedirectToAction("Index");
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Chat chatById = listChats.SingleOrDefault(c => c.Id == id);
                if (chatById != null)
                {
                    listChats.Remove(chatById);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
