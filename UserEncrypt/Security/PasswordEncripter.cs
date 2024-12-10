﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace UserEncrypt.Security
{
    public class PasswordEncripter : IPasswordEncripter
    {
        public string Encript(string password, List<byte[]> keys)
        {
            // Se verifica que la lista de claves tenga al menos dos elementos: hashKey y hashIV
            if (keys == null || keys.Count < 2)
                throw new ArgumentException("HashKey and HashIV are required.");
            // Se asigna la clave de encriptación (hashKey) y el vector de inicialización (hashIV) a las variables correspondientes
            byte[] hashKey = keys[0];
            byte[] hashIV = keys[1];

            // Se crea una instancia del algoritmo AES
            // Se usa using para que los recursos asociados con el objeto AES se liberen al finalizar la encriptacion
            using (Aes aesAlg = Aes.Create())
            {
                // Se establece la clave de encriptación y el vector de inicialización
                aesAlg.Key = hashKey;
                aesAlg.IV = hashIV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // Se convierte la contraseña en un arreglo de bytes usando UTF - 8, ya que AES trabaja con datos binarios
                byte[] inputBuffer = Encoding.UTF8.GetBytes(password);
                // realiza la encriptación de los bytes de la contraseña devolviendo el resultado cifrado como un arreglo de bytes
                byte[] encrypted = encryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                // Se Convierte el resultado encriptado (byte[]) a una cadena en formato Base64 y la devolvemos en forma de texto
                return Convert.ToBase64String(encrypted);
            }
        }
        public string Decript(string encryptedPassword, List<byte[]> keys)
        {
            // Verifica que la lista de claves tenga al menos dos elementos: hashKey y hashIV
            if (keys == null || keys.Count < 2)
                throw new ArgumentException("HashKey and HashIV are required.");

            byte[] hashKey = keys[0];  // La clave de encriptación
            byte[] hashIV = keys[1];   // El vector de inicialización

            // Se crea una instancia del algoritmo AES
            using (Aes aesAlg = Aes.Create())
            {
                // Se establece la clave de encriptación y el vector de inicialización
                aesAlg.Key = hashKey;
                aesAlg.IV = hashIV;

                // Crea el transformador para descifrar
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Convierte la cadena cifrada en base64 a un arreglo de bytes
                byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);

                // Descifra los bytes
                byte[] decrypted = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                // Convierte los bytes descifrados a una cadena usando UTF-8
                return Encoding.UTF8.GetString(decrypted);
            }
        }

    }
}