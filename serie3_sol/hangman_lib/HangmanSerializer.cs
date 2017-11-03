using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace hangman_lib
{
    public class HangmanSerializer
    {
        public static Hangman Deserialize(string _path)
        {
            Hangman hangman = null;
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    hangman = (Hangman) formatter.Deserialize(stream);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return hangman;
        }

        public static bool Serialize(string _path, Hangman _hangman)
        {
            bool success = true;
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(_path, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    formatter.Serialize(stream, _hangman);
                    stream.Flush();
                }
                catch (ArgumentNullException e)
                {
                    success = false;
                    Console.WriteLine(e.Message);
                }
                catch (SerializationException e)
                {
                    success = false;
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }
    }
}