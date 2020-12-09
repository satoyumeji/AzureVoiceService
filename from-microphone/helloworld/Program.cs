//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

// <code>
using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace helloworld
{
    class Program
    {
        public static async Task RecognizeSpeechAsync()
        {
            // 接続APIキーとロケール
            // URIでも指定ができる
            // 参考　https://docs.microsoft.com/en-us/dotnet/api/microsoft.cognitiveservices.speech.speechconfig?view=azure-dotnet
            var config = SpeechConfig.FromSubscription("API_KEY", "japaneast");


            // 言語の指定
            // 参考ロケール　https://dita-jp.org/webhelp-feedback/about_general_topic/topic_locale.html　
            var sourceLanguageConfig = SourceLanguageConfig.FromLanguage("ja-jp");

            // レコグナイザー（音声識別クラス）の作成
            using (var recognizer = new SpeechRecognizer(config,sourceLanguageConfig))
            {
                Console.WriteLine("Say something...");

                // 録音開始
                var result = await recognizer.RecognizeOnceAsync();

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    // 正常識別結果
                    Console.WriteLine($"We recognized: {result.Text}");
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    // 音声と言語が見つからなかった結果
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    // その他エラーの結果
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
            }
        }

        static async Task Main()
        {
            await RecognizeSpeechAsync();
            Console.WriteLine("Please press <Return> to continue.");
            Console.ReadLine();
        }
    }
}
// </code>
