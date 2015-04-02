using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DeliveryIngesup.Manager
{
    public static class PaiementManager
    {
        public static bool IsValidCardNumber(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "");

            //FIRST STEP: Double each digit starting from the right
            int[] doubledDigits = new int[cardNumber.Length / 2];
            int k = 0;
            for (int i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int digit = int.Parse(cardNumber[i].ToString());
                doubledDigits[k] = digit * 2;
                k++;
            }

            //SECOND STEP: Add up separate digits
            int total = 0;
            foreach (int i in doubledDigits)
            {
                string number = i.ToString();
                for (int j = 0; j < number.Length; j++)
                {
                    total += int.Parse(number[j].ToString());
                }
            }

            //THIRD STEP: Add up other digits
            int total2 = 0;
            for (int i = cardNumber.Length - 1; i >= 0; i -= 2)
            {
                int digit = int.Parse(cardNumber[i].ToString());
                total2 += digit;
            }

            //FOURTH STEP: Total
            int final = total + total2;

            return final % 10 == 0; //Well formed will divide evenly by 10
        }


        //Merchant credentials

        //private static string _token;

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(".::. PAYPAL REST API PAYMENT EXAMPLE .::.");
        //    Console.WriteLine();
        //    Console.WriteLine("Please wait, we are retriving the token from PayPal.....");
        //    Console.WriteLine();

        //    //Printing the access token
        //    PrintToken();
        //    //Establishing the PayPal API call
        //    Console.WriteLine("*************************************************************************");
        //    Console.WriteLine();
        //    Console.Write("Do you want to test the Payment? YES or NO : ");
        //    string x = Console.ReadLine();

        //    if (x == "YES" || x == "yes" || x == "y" || x == "Y")
        //    {
        //        Console.WriteLine();
        //        Console.WriteLine("*************************************************************************");
        //        Console.WriteLine();
        //        PaymentRequest();
        //        Console.WriteLine();
        //        Console.WriteLine("*************************************************************************");
        //        Console.WriteLine();
        //    }
        //    else
        //    {
        //        Console.WriteLine();
        //        Console.WriteLine("*************************************************************************");
        //        Console.WriteLine();
        //        Console.WriteLine("Press any button to close the application");
        //    }

        //    //Keeping the console open
        //    Console.ReadLine();
        //}

        //Retriving the Token
        //private static string GetPayPalToken()
        //{
        //    string clientID = "AVHyjNcHusoeflvyYKjGHE_q8gGCS9-5aoBpDpgxWwhKUy7CWq92HPvuPJC2YBHB2Q3yIQPSNu3yjZQD";
        //    string clientSecret = "EEXvh50LOB03Q4bI2StiFp1Fx0ZWFJvC_4EjQ7s9ZVVl3BHsYdXdg1x5wkh-VYSRZv-IOBd17vr6ERj7";

        //    OAuthTokenCredential tokenCredential = new OAuthTokenCredential(clientID, clientSecret);
        //    var token = tokenCredential.GetAccessToken();

        //    return token;
        //}

        //Printing the token
        //public static void PrintToken()
        //{
        //    //Getting the access token

        //    token = GetPayPalToken();

        //    if (token.Contains("Token Error"))
        //    {
        //        Console.WriteLine("A runtime exception has been generated, please check your internet connectivity and try again");
        //    }
        //    else
        //    {
        //        Console.WriteLine("*************************************************************************");
        //        Console.WriteLine("PayPal Token = " + token);
        //    }
        //}

        //Establishing the Payment
        //    public static void PaymentRequest()
        //    {
        //        try
        //        {
        //            var token = GetPayPalToken();

        //            if (PaiementManager.GetPayPalToken().Contains("Token Error"))
        //            {
        //                //TODO : Erreur
        //            }
        //            else
        //            {
        //                Payment p = new Payment()
        //                {
        //                    intent = "sale",
        //                    payer = new Payer
        //                    {
        //                        payment_method = "credit_card",

        //                        funding_instruments = new List<FundingInstrument>
        //                        {
        //                            new FundingInstrument
        //                            {
        //                                credit_card = new CreditCard
        //                                {
        //                                    type = "visa",
        //                                    number = "4032031399409761",
        //                                    expire_month = 2,
        //                                    expire_year = 2020,
        //                                    //cvv2 = "222",
        //                                    first_name = "test",
        //                                    last_name = "buyer",

        //                                    //Adresse de facturation
        //                                    billing_address = new Address
        //                                    {
        //                                        line1 = "1 Main ST",
        //                                        city = "San Jose",
        //                                        country_code = "US",
        //                                        postal_code = "95131",
        //                                        state = "CA",
        //                                    },
        //                                },
        //                            },
        //                        },
        //                    },

        //                    transactions = new List<Transaction>
        //                    {
        //                        new Transaction
        //                        {
        //                            amount = new Amount
        //                            {
        //                                total = "10",
        //                                currency = "USD"
        //                            },
        //                            description = "This is the payment transaction description"
        //                        }

        //                    },

        //                }.Create(token);
        //            }

        //            //Console.WriteLine("Payment Completed: \n\r\n\r");
        //            //Console.WriteLine(p.ConvertToJson());
        //        }
        //        catch (Exception e)
        //        {
        //            //Console.WriteLine("RUNTIME EXCEPTION" + e);
        //        }

        //    }
    }
}
