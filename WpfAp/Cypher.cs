using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAp
{
    public class Cipher
    {
        const string defaultAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЪЭЮЯ";
        public readonly string letters;
        public Cipher(string alphabet = null)
        {
            letters = string.IsNullOrEmpty(alphabet) ? defaultAlphabet : alphabet;
        }
        //Получаем ключ длинной с текст
        private string GetRepeatKey(string text,string pass)
        {
            
            string pa = pass;
            text = text.ToUpper();
            while (pa.Length < text.Length)
                pa += pass;

            for (int i=0;i<text.Length;i++)
            {
            var letterI = letters.IndexOf(text[i]);                
                if (letterI<0)
                    pa = pa.Insert(i, " ");
            }

            return pa;
        }
        //шифрование\дешифрование
        private string Vigenere(string text, string password, bool encrypt = true)
        {

            var gamma = GetRepeatKey( text,password);
            var retValue = "";
            var q = letters.Length;

            for (int i = 0; i < text.Length; i++)
            {
                var letterI = letters.IndexOf(text[i]);
                var codeI = letters.IndexOf(gamma[i]);
                
                if (letterI<0)
                {
                    //если буква не найдена, добавляем её в исходном виде
                    retValue += text[i].ToString(); 
                }
                else
                {
                    retValue  += letters[(q + letterI + ((encrypt ? 1 : -1) *codeI)) % q].ToString();
                }
            }

            return retValue;
        }

        //шифрование текста
        public string Encrypt(string plainMessage, string password)
            => Vigenere(plainMessage, password);

        //дешифрование текста
        public string Decrypt(string encryptedMessage, string password)
            => Vigenere(encryptedMessage, password, false);
    }
}
