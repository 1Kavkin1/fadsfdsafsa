using Abonents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

string filePath = "abonents.txt";

Console.WriteLine("--Додавання нового абонента--");

List<Abonent> items = new List<Abonent>();

Console.WriteLine("Вкажіть ПІБ:");
string pib = Console.ReadLine();

Console.WriteLine("Вкажіть телефон:");
string phone = Console.ReadLine();

Console.WriteLine("Вкажіть адресу:");
string address = Console.ReadLine();

Abonent newAbonent = new Abonent(pib, phone, address);
items.Add(newAbonent);

// 1. ЗАПИС У ФАЙЛ
// StreamWriter з параметром 'true' дозволяє дописувати нові дані у кінець файлу
using (StreamWriter writer = new StreamWriter(filePath, append: true, encoding: Encoding.UTF8))
{
    writer.WriteLine(newAbonent.ToFileString());
}
Console.WriteLine("\n[Успішно] Дані збережено у файл!");

// 2. ЧИТАННЯ З ФАЙЛУ
Console.WriteLine("\n==== Список абонентів з файлу ===");

List<Abonent> loadedAbonents = new List<Abonent>();

if (File.Exists(filePath))
{
    string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

    foreach (string line in lines)
    {
        if (!string.IsNullOrWhiteSpace(line))
        {
            // Створюємо об'єкт із рядка файлу
            Abonent abonent = Abonent.FromFileString(line);
            loadedAbonents.Add(abonent);
        }
    }

    // Вивід зчитаного списку
    foreach (var item in loadedAbonents)
    {
        Console.WriteLine(item);
    }
}
else
{
    Console.WriteLine("Файл ще не створено.");
}


namespace Abonents
{
    class Abonent
    {
        private string _pib;
        private string _phone;
        private string _address;

        // Конструктор за замовчуванням
        public Abonent()
        {
            _pib = "Немає ПІБ";
            _address = "Відсутня";
            _phone = "----";
        }

        // Конструктор з параметрами
        public Abonent(string pib, string phone, string address)
        {
            _pib = pib;
            _phone = phone;
            _address = address;
        }

        // Метод для перетворення об'єкта в рядок для файлу (роздільник ;)
        public string ToFileString()
        {
            return $"{_pib};{_phone};{_address}";
        }

        // Статичний метод для створення об'єкта із рядка файлу
        public static Abonent FromFileString(string line)
        {
            string[] parts = line.Split(';');
            if (parts.Length == 3)
            {
                return new Abonent(parts[0], parts[1], parts[2]);
            }
            return new Abonent();
        }

        public override string ToString()
        {
            string info = $"ПІБ: {_pib}\tТелефон: {_phone}\tАдреса: {_address}";
            return info;
        }
    }
}