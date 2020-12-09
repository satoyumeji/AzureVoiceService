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
            // �ڑ�API�L�[�ƃ��P�[��
            // URI�ł��w�肪�ł���
            // �Q�l�@https://docs.microsoft.com/en-us/dotnet/api/microsoft.cognitiveservices.speech.speechconfig?view=azure-dotnet
            var config = SpeechConfig.FromSubscription("API_KEY", "japaneast");


            // ����̎w��
            // �Q�l���P�[���@https://dita-jp.org/webhelp-feedback/about_general_topic/topic_locale.html�@
            var sourceLanguageConfig = SourceLanguageConfig.FromLanguage("ja-jp");

            // ���R�O�i�C�U�[�i�������ʃN���X�j�̍쐬
            using (var recognizer = new SpeechRecognizer(config,sourceLanguageConfig))
            {
                Console.WriteLine("Say something...");

                // �^���J�n
                var result = await recognizer.RecognizeOnceAsync();

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    // ���펯�ʌ���
                    Console.WriteLine($"We recognized: {result.Text}");
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    // �����ƌ��ꂪ������Ȃ���������
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    // ���̑��G���[�̌���
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
