using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NLPCloud_IO_CSharp_Sample
{
    //Build Note: This sample was created with .NET 4.7.1 Framework, change project property settings for your desired .NET framework version.
    
    class Program
    {
        static async Task Main(string[] args)
        {
           
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await MainMenuAsync();
            }
           
        }
        /// <summary>
        /// Private access token provided by https://nlpcloud.io/ for use with your account.
        /// </summary>
        private static string MyAccessToken = "Your access token";
        private static async Task<bool> MainMenuAsync()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Reminder: 3 calls per minute with free account");
            Console.ResetColor();
          
            Console.WriteLine("Choose an option:");
           
            Console.WriteLine("1) FaceBookBartLargeCNN:                 Summarize a text using facebook's Bart Large CNN model ");
            Console.WriteLine("2) NER:                                  Perform Named Entity Recognition (NER) using spaCy's large English model");
            Console.WriteLine("3) FaceBookBartLargeMNLI:                Classify a text using Facebook's Bart Large MNLI model");
            Console.WriteLine("4) DistilbertBaseUncasedFinetunedSST2:   Analyze sentiment of a text using Distilbert Base Uncased Finetuned SST 2 model");
            Console.WriteLine("5) RobertaBaseSquad2:                    Answer questions with contexts using Roberta's Base Squad 2 model");
            Console.WriteLine("6) HelsinkiNLPOpusMTEnglishtoFrench:     Translate a text using Helsinki NLP's Opus MT English to French model");
            Console.WriteLine("7) DetectLanguagesPythonLangDetect:      Detect languages in a text using Python's LangDetect library");


            Console.WriteLine("8) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    await FaceBookBartLargeCNN();
                    return true;
                case "2":
                    await NER();
                    return true;
                case "3":
                    await FaceBookBartLargeMNLI();
                    return true;
                case "4":
                    await DistilbertBaseUncasedFinetunedSST2();
                    return true;
                case "5":
                    await RobertaBaseSquad2();
                    return true;
                case "6":
                    await HelsinkiNLPOpusMTEnglishtoFrench();
                    return true;
                case "7":
                    await DetectLanguagesPythonLangDetect();
                    return true;

                case "8":
                    return false;
                default:
                    return true;
            }
        }

        // Source:  https://nlpcloud.io/
        // * 3 requests per minute with free account
        // From https://nlpcloud.io/:
        // "Your block of text cannot be bigger than 1024 tokens, otherwise you will get a HTTP 400 error.Also note that this model works best for blocks of text between 56 and 142 tokens, and it might take several seconds to respond due to the complex computation needed.
        // If you need to count the tokens in your text, you can use our "Tokens" API endpoint."

        // With just three requests per minute we are running the Tasks individually, the below does not apply with these tests.
        // In production, we will create one HttpClient then run the tasks with the same HttpClient. See link below for sample. 
        // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
        // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/


        // Summarize a text using facebook's Bart Large CNN model:
        private static async Task FaceBookBartLargeCNN()
        {
            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.nlpcloud.io/v1/bart-large-cnn/summarization"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token "+ MyAccessToken);

                    request.Content = new StringContent("{\"text\":\"The tower is 324 metres (1,063 ft) tall, about the same height as an 81-storey building, and the tallest structure in Paris. Its base is square, measuring 125 metres (410 ft) on each side. During its construction, the Eiffel Tower surpassed the Washington Monument to become the tallest man-made structure in the world, a title it held for 41 years until the Chrysler Building in New York City was finished in 1930. It was the first structure to reach a height of 300 metres. Due to the addition of a broadcasting aerial at the top of the tower in 1957, it is now taller than the Chrysler Building by 5.2 metres (17 ft). Excluding transmitters, the Eiffel Tower is the second tallest free-standing structure in France after the Millau Viaduct.\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
                    var response = await httpClient.SendAsync(request);
                    var output = response.Content.ReadAsStringAsync();
                    Console.WriteLine(output.Result);

                    ShowMenueOptionsAfterExecution();
                }
            }

        }
        private static void ShowMenueOptionsAfterExecution()
        {
            Console.WriteLine(Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Press Any Key For Menu");
            Console.ReadLine();
        }
       
        // Perform Named Entity Recognition(NER) using spaCy's large English model: 
        private static async Task NER()
        {
            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.nlpcloud.io/v1/en_core_web_lg/entities"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token " + MyAccessToken);

                    request.Content = new StringContent("{\"text\":\"John Doe has been working for Microsoft in Seattle since 1999.\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    var output = response.Content.ReadAsStringAsync();
                    Console.WriteLine(output.Result);
                    ShowMenueOptionsAfterExecution();
                }
            }
        }
       // Classify a text using Facebook's Bart Large MNLI model:
        private static async Task FaceBookBartLargeMNLI()
        {
            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.nlpcloud.io/v1/bart-large-mnli/classification"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token " + MyAccessToken);

                    request.Content = new StringContent("{\n    \"text\":\" Microsoft introduced the preview of ML.NET (Machine Learning .NET) which is a cross-platform, open-source Machine Learning Framework. Yes, now it is easy to develop our own Machine Learning application or develop custom modules using Machine Learning Framework. ML.NET is a Machine Learning framework that was mainly developed for .NET developers.\",\n    \"labels\":[\"c#\", \"programming\",\"database\", \"machine learning\",\"source control\"],\n    \"multi_class\": true\n}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    var output = response.Content.ReadAsStringAsync();
                    Console.WriteLine(output.Result);
                    ShowMenueOptionsAfterExecution();
                }
            }
        }
        //Analyze sentiment of a text using Distilbert Base Uncased Finetuned SST 2 model:
        private static async Task DistilbertBaseUncasedFinetunedSST2()
        {
            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.nlpcloud.io/v1/distilbert-base-uncased-finetuned-sst-2-english/sentiment"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token " + MyAccessToken);

                    request.Content = new StringContent("{\"text\":\"NLP Cloud proposes an amazing service!\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    var output = response.Content.ReadAsStringAsync();
                    Console.WriteLine(output.Result);
                    ShowMenueOptionsAfterExecution();
                }
            }
        }

        //Answer questions with contexts using Roberta's Base Squad 2 model:
        private static async Task RobertaBaseSquad2()
        {
            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.nlpcloud.io/v1/roberta-base-squad2/question"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token " + MyAccessToken);

                    request.Content = new StringContent("{\n    \"context\":\"French president Emmanuel Macron said the country was at war with an invisible, elusive enemy, and the measures were unprecedented, but circumstances demanded them.\",\n    \"question\":\"Who is the French president?\"\n}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    var output = response.Content.ReadAsStringAsync();
                    Console.WriteLine(output.Result);
                    ShowMenueOptionsAfterExecution();
                }
            }
        }
        //Translate a text using Helsinki NLP's Opus MT English to French model:
        private static async Task HelsinkiNLPOpusMTEnglishtoFrench()
        {
            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.nlpcloud.io/v1/opus-mt-en-fr/translation"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token " + MyAccessToken);

                    request.Content = new StringContent("{\"text\":\"John Doe has been working for Microsoft in Seattle since 1999.\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    var output = response.Content.ReadAsStringAsync();
                    Console.WriteLine(output.Result);
                    ShowMenueOptionsAfterExecution();
                }
            }

        }

        // Detect languages in a text using Python's LangDetect library:
        private static async Task DetectLanguagesPythonLangDetect()
        {
            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.nlpcloud.io/v1/python-langdetect/langdetection"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token "+ MyAccessToken);

                    request.Content = new StringContent("{\"text\":\"John Doe has been working for Microsoft in Seattle since 1999. Il parle aussi un peu français.\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    var output = response.Content.ReadAsStringAsync();
                    Console.WriteLine(output.Result);
                    ShowMenueOptionsAfterExecution();
                }
            }
        }
      
      //end Program
    }
}
