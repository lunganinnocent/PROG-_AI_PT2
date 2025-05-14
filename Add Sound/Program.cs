using static System.Console;
using System.Media;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace PROG_Assignment
{
    class AddSound
    {
        static string userName = "";
        static string favoriteTopic = "";
        static Random rand = new Random();
        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string>() {
                "Use strong, unique passwords for each account.",
                "Avoid using personal details in your passwords.",
                "Consider using a password manager to keep track of complex passwords."
            }},
            { "scam", new List<string>() {
                "Scammers often impersonate trusted organizations. Always verify before responding.",
                "Don't click on suspicious links or attachments from unknown sources.",
                "Check for spelling and grammar errors, which are common in scam messages."
            }},
            { "privacy", new List<string>() {
                "Regularly review privacy settings on social media.",
                "Limit what personal information you share online.",
                "Use encrypted messaging apps for sensitive communication."
            }},
            { "phishing", new List<string>() {
                "Be cautious of emails asking for personal info.",
                "Check sender addresses and avoid clicking suspicious links.",
                "Phishing emails often create a sense of urgency to trick you."
            }},
            { "encryption", new List<string>() {
                "Encryption is the process of converting information or data into code, especially to prevent unauthorised access.",
                "To troubleshoot encryption, use end-to-end encrypted messaging apps.",
                "Take actions to make sure you are as safe as you can be.",
                "Switch to using messaging apps that offer end-to-end encryption, e.g., WhatsApp, Signal, Thread. Some are better than others, so make sure to read the reviews.",
                "Turn on encryption on your device. Some devices will offer encryption but not set it as the default. Make sure to turn it on.",
                "Encryption can only be as good as the other security tools it relies on, so taking the following security steps is critical: use strong passwords."
            }},
            { "how are you", new List<string>() {
                "I'm just a bot, but I'm doing great, thanks for asking!"
            }}
        };
        static Dictionary<string, string> emotionResponses = new Dictionary<string, string>()
        {
            { "worried", "It's completely understandable to feel that way. Let me help you stay safe. :)" },
            { "curious", "I love your curiosity! Let's explore more about this topic together. :)" },
            { "frustrated", "I know it can be confusing, but don't worry, you're doing great learning this stuff. :)" },
            { "happy", "I'm glad you're feeling good! Cybersecurity can be empowering. :)" },
            { "sad", "I'm here to support you. Let's focus on learning how to stay secure." },
            { "angry", "It's okay to be upset. Let's figure out how to prevent issues together." }
        };
        static Dictionary<string, string> emotionKeywords = new Dictionary<string, string>()
        {
            { "anxious", "worried" },
            { "nervous", "worried" },
            { "confused", "frustrated" },
            { "irritated", "frustrated" },
            { "mad", "angry" },
            {"angry","angry" },
            {"sad","sad" },
            {"unhappy","sad" },
            {"happy","happy" },
            {"exited","happy" },
            {"interested","curious" },
            {"curious","curious" },
            {"worried","worried" },
            {"frustrated","worried" }
        };

        static void Main(string[] args) // Main method
        {
            // Play sound if on Windows
            if (OperatingSystem.IsWindows())
            {
                try
                {
                    SoundPlayer MyMusic = new SoundPlayer("WhatsApp Audio 2025-03-06 at 12.28.54_10ecb551.wav");
                    MyMusic.Load();
                    MyMusic.PlaySync();
                    MyMusic.Dispose();
                }
                catch (Exception ex)
                {
                    WriteLine($"Error playing sound: {ex.Message}. Make sure the sound file exists.");
                }
            }

            // Get desired image dimensions
            int imageWidth = 500; // Default width
            int imageHeight = 150; // Default height

            WriteLine("Do you want to customize the image size? (yes/no)");
            if (ReadLine()?.ToLower() == "yes")
            {
                WriteLine("Enter the desired width for the image:");
                if (!int.TryParse(ReadLine(), out imageWidth))
                {
                    WriteLine("Invalid width. Using default width of 500.");
                    imageWidth = 500;
                }

                WriteLine("Enter the desired height for the image:");
                if (!int.TryParse(ReadLine(), out imageHeight))
                {
                    WriteLine("Invalid height. Using default height of 150.");
                    imageHeight = 150;
                }
            }

            // Display the single image
            DisplaySingleImage(imageWidth, imageHeight);

            // Cybersecurity Chatbot Interaction
            WriteLine("\nWelcome to the Cybersecurity Chatbot!");
            WriteLine("My name is CyberBot. What's your name?");
            userName = ReadLine();

            WriteLine($"Nice to meet you, {userName}! What's your favorite cybersecurity topic? (e.g., password, scam, privacy, phishing, encryption)");
            favoriteTopic = ReadLine()?.ToLower();

            if (keywordResponses.ContainsKey(favoriteTopic))
            {
                WriteLine($"Great! Here's some information about {favoriteTopic}:");
                foreach (string response in keywordResponses[favoriteTopic])
                {
                    WriteLine($"- {response}");
                }
            }
            else
            {
                WriteLine($"I'm sorry, I don't have specific information about '{favoriteTopic}'. However, I can still help you with other cybersecurity topics.");
            }

            WriteLine("How are you feeling today? (e.g., worried, curious, frustrated, happy, sad, angry)");
            string userEmotion = ReadLine()?.ToLower();

            if (emotionKeywords.ContainsKey(userEmotion))
            {
                userEmotion = emotionKeywords[userEmotion];
            }

            if (emotionResponses.ContainsKey(userEmotion))
            {
                WriteLine(emotionResponses[userEmotion]);
            }
            else
            {
                WriteLine("Thanks for sharing your feelings. I'm here to help you with cybersecurity.");
            }

            string continueChat;
            do
            {
                WriteLine("Is there anything else I can help you with? (yes/no)");
                continueChat = ReadLine()?.ToLower();

                if (continueChat == "yes")
                {
                    WriteLine("What else can I help you with? (e.g., password, scam, privacy, phishing, encryption)");
                    string nextTopic = ReadLine()?.ToLower();

                    if (keywordResponses.ContainsKey(nextTopic))
                    {
                        WriteLine($"Here's some information about {nextTopic}:");
                        foreach (string response in keywordResponses[nextTopic])
                        {
                            WriteLine($"- {response}");
                        }
                    }
                    else
                    {
                        WriteLine($"I'm sorry, I don't have specific information about '{nextTopic}'. I can still help with other topics.");
                    }
                }
                else if (continueChat != "no")
                {
                    WriteLine("Invalid input. Please answer 'yes' or 'no'.");
                }
            } while (continueChat == "yes");

            WriteLine("Thank you for chatting with me! Stay safe online!");
        }

        static void DisplaySingleImage(int width, int height)
        {
            string imagePath = "corgi.jpg";
            Bitmap image = null;

            try
            {
                image = new Bitmap(imagePath);
                WriteLine($"Loaded image: {imagePath}");
                WriteLine($"Original image size: {image.Width}x{image.Height}");
                WriteLine($"Resizing image to: {width}x{height}");
                WriteLine($"ASCII representation of the {width}x{height} image:");

                Bitmap resizedImage = ResizeImage(image, width, height);
                Bitmap grayscaleImage = ConvertToGrayscale(resizedImage);
                DisplayImageAsASCII(grayscaleImage);
            }
            catch (Exception ex)
            {
                WriteLine($"Error loading or processing image '{imagePath}': {ex.Message}. Make sure the image file exists in the correct location.");
            }
            finally
            {
                image?.Dispose();
                WriteLine("Press any key to continue to the chatbot...");
                ReadKey();
            }
        }

        static Bitmap ResizeImage(Bitmap original, int width, int height)
        {
            Bitmap resized = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, 0, 0, width, height);
            }
            return resized;
        }

        static Bitmap ConvertToGrayscale(Bitmap original)
        {
            Bitmap grayscale = new Bitmap(original.Width, original.Height);
            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    grayscale.SetPixel(x, y, grayColor);
                }
            }
            return grayscale;
        }

        static void DisplayImageAsASCII(Bitmap img)
        {
            string asciiChars = "@%#*+-:.";
            int yStep = Math.Max(1, img.Height / 50);
            int xStep = Math.Max(1, img.Width / 150);

            for (int y = 0; y < img.Height; y += yStep)
            {
                for (int x = 0; x < img.Width; x += xStep)
                {
                    Color pixel = img.GetPixel(x, y);
                    int grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    char asciiChar = asciiChars[grayValue * (asciiChars.Length - 1) / 255];
                    Write(asciiChar);
                }
                WriteLine();
            }
        }
    }
}