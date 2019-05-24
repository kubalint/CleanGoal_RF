using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Mvc;
using Persistence;
using Persistence.Model;

namespace App.Models
{
    public class HelperMethods
    {
        public static Dictionary<Product,int> GetBasketEntriesToId(string id)
        {
            Dictionary<Product, int> productList = new Dictionary<Product, int>();

            using (StoreContext db = new StoreContext())
            {
                List<BasketEntry> basketEntries = db.BasketEntries.Where(x => x.UserID == id).ToList();
                
                foreach (BasketEntry entry in basketEntries)
                {
                    Product product = db.Products.Where(x => x.ID.ToString() == entry.ProductID).FirstOrDefault();
                    productList.Add(product, entry.Quantity);
                }
            }
           
            return productList;
        }

        public static Dictionary<Product, int> GetOrderListFromId(string id, string orderId)
        {
            Dictionary<Product, int> productList = new Dictionary<Product, int>();

            using (StoreContext db = new StoreContext())
            {
                Order order = db.Orders.SingleOrDefault(x => x.UserID == id && x.OrderId==orderId);
                
                foreach (OrderItem item in order.Items)
                {
                    Product product = db.Products.FirstOrDefault(x => x.ID.ToString() == item.ProductID);
                    productList.Add(product, item.Quantity);
                }
            }
            
            return productList;
        }

        public static string GetUserID(IPrincipal user, HttpSessionStateBase session)
        {
            if (user.Identity.IsAuthenticated)
            {
                return user.Identity.GetUserId();
            } else
            {
                return session.SessionID;
            }
        }

        public static void CopyBasketEntries(string userID, HttpSessionStateBase session)
        {
            
            using (StoreContext db = new StoreContext())
            {
                List<BasketEntry> basketEntries = db.BasketEntries.Where(x => x.UserID == session.SessionID).ToList();

                foreach (BasketEntry entry in basketEntries)
                {
                    if (db.BasketEntries.Where(x => x.UserID == userID && x.ProductID == entry.ProductID.ToString()).Count() == 0)
                    {
                        BasketEntry newEntry = new BasketEntry()
                        {
                            UserID = userID,
                            ProductID = entry.ProductID.ToString(),
                            Quantity = 1
                        };
                        db.BasketEntries.Add(newEntry);
                    }
                    else
                    {
                        BasketEntry old = db.BasketEntries.Where(x => x.UserID == userID && x.ProductID == entry.ProductID.ToString()).FirstOrDefault();
                        BasketEntry newEntry = new BasketEntry { UserID = old.UserID, ProductID = old.ProductID, Quantity = old.Quantity };
                        newEntry.Quantity++;

                        db.BasketEntries.Remove(old);
                        db.BasketEntries.Add(newEntry);
                    }



                    //db.BasketEntries.Add(new BasketEntry()
                    //{
                    //    UserID = user.Identity.GetUserId(),
                    //    ProductID = entry.ProductID,
                    //    Quantity=entry.Quantity
                    //});

                }

                foreach (BasketEntry entry in basketEntries)
                {
                    db.BasketEntries.Remove(entry);
                }

                db.SaveChanges();


            }
            
        }


        

    }
}