﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeWork1.Models;
using System.Data.Entity.Validation;
using HomeWork1.ActionFilters;
using PagedList.Mvc; //import this so we get our HTML Helper
using PagedList; //import this so we can cast our list to IPagedList (only necessary because ViewBag is dynamic)

namespace HomeWork1.Controllers
{
    public class 客戶聯絡人Controller :BaseController
    {
        // GET: 客戶聯絡人
        [TimerFilter]
        public ActionResult Index(int? page)
        {
            var 客戶聯絡人 = this.Repo客戶聯絡人.All().Include("客戶資料").ToList();
            return View(客戶聯絡人.ToPagedList(page ?? 1, 5));
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = this.Repo客戶聯絡人.Where(x => x.Id == id).FirstOrDefault();
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(this.Repo客戶資料.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                this.Repo客戶聯絡人.Add(客戶聯絡人);
                this.Repo客戶聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(this.Repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = this.Repo客戶聯絡人.Where(x => x.Id == id).FirstOrDefault();

            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(this.Repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                this.Repo客戶聯絡人.UnitOfWork.Context.Entry(客戶聯絡人).State = System.Data.Entity.EntityState.Modified;
                this.Repo客戶聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(this.Repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = this.Repo客戶聯絡人.Where(x => x.Id == id).FirstOrDefault();
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = this.Repo客戶聯絡人.Where(x => x.Id == id).FirstOrDefault();
            客戶聯絡人.是否刪除 = true;
            try
            {
                this.Repo客戶聯絡人.UnitOfWork.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                //throw ex;
                var allErrors = new List<string>();

                foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in re.ValidationErrors)
                    {
                        allErrors.Add(err.ErrorMessage);
                    }
                }

                ViewBag.Errors = allErrors;
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Repo客戶聯絡人.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
