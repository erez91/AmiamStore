using AmiamStore.App_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmiamStore.Models
{
    public class CartModel
    {
        public ProductModel product { get; set; }
        public int? Quantity { get; set; }

        public CartModel(ProductModel p , int q)
        {
            this.product = p;
            this.Quantity = q;
        }
    }

    public class CartViewModel
    {
        [Required(ErrorMessage = "Card Holder is mandatory.")]
        [StringLength(20)]
        public string CardHolder { get; set; }
        [Required(ErrorMessage = "Card Number is mandatory.")]
        [StringLength(17)]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "cvv Number is mandatory.")]
        [StringLength(3)]
        public string Cvv { get; set; }
        public double OrderAmount { get; set; }
        [Required(ErrorMessage = "Expiry Date is mandatory.")]
        public string ExpiryDate { get; set; }
        //public int UserID { get; set; }
        public int LineOfBalance { get; set; }

        public List<CartModel> Products { get; set; }

    }
}