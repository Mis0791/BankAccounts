using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAccounts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Controllers
{
    public class AccountController : Controller
    {
        private BankContext _context;

        public AccountController(BankContext context)
        {
            _context = context;
        }

        private RegisterUser ActiveUser
        {
            get{ return _context.User.Where( u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();}
        }

        public IActionResult Index()
        {
            if(ActiveUser == null)
            return RedirectToAction("Index", "Home");

            RegisterUser thisUser = _context.User.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();
            List<Account> allTransactions = _context.Account.Where(a => a.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
            ViewBag.results = allTransactions;
            ViewBag.UserInfo = thisUser;
            if(TempData["error"] != null){
                ViewBag.error = TempData["error"];
            }
            return View();
        }

        [HttpPost]
        [Route ("addtransaction")]
        public IActionResult addTransaction(Account account)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            int uid = (int)id;
            Account newTransaction = new Account()
            {
                UserId = uid,
                Transaction = account.Transaction
            };
            RegisterUser thisUser = _context.User.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();
            thisUser.Balance += account.Transaction;
            if(account.Transaction * -1 > thisUser.Balance)
            {
                List<Account> allTransactions = _context.Account.Where(a => a.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
                ViewBag.results = allTransactions;
                TempData["error"] = "Withdraw exceeds balance!";
            } else {
            _context.Account.Add(newTransaction);
            _context.SaveChanges();
            }
            return RedirectToAction("Index");  
        }

        [HttpGet]
        [Route ("logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }


    }
}
