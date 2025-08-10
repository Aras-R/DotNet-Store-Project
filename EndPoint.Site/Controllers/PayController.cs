using Dto.Payment;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;
using Store.Application.Services.Fainances.Commands.AddRequestPay;
using Store.Application.Services.Fainances.Queries.GetRequestPayService;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Finances;
using Store.Persistence.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarinPal.Class;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class PayController : Controller
    {
        private readonly IAddRequestPayService _addRequestPayService;
        private readonly IGetRequestPayService _getRequestPayService;
        private readonly ICartService _cartService;
        private readonly CookiesManeger _cookiesManeger;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        
        public PayController(IAddRequestPayService addRequestPayService
            , ICartService cartService
            , IGetRequestPayService getRequestPayService


             )
        {
            _addRequestPayService = addRequestPayService;
            _cartService = cartService;
            _cookiesManeger = new CookiesManeger();
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            _getRequestPayService = getRequestPayService;

        }
        public async Task<IActionResult> Index()
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            if (cart.Data.SumAmount > 0)
            {
                var requestPay = _addRequestPayService.Execute(cart.Data.SumAmount, UserId.Value);
                // ارسال در گاه پرداخت


                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09121112222",
                    CallbackUrl = $"https://localhost:44367/Pay/Verify?guid={requestPay.Data.guid}",
                    Description = "پرداخت فاکتور شماره:" + requestPay.Data.RequestPayId,
                    Email = requestPay.Data.Email,
                    Amount = requestPay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}"); 



            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }

        }





        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {

            var requestPay = _getRequestPayService.Execute(guid);

            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = requestPay.Data.Amount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);

            if (verification.Status == 100)
            {

            }
            else
            {

            }

            return View();
        }
    }
}
