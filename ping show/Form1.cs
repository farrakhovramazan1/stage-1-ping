using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Runtime.InteropServices;
namespace ping_show
{
    public partial class Form1 : Form
    {
        public Form1() //Конструктор формы, описывать нечего
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//Происходит при событии нажатия пользователем на кнопку
        {
            Thread t = new Thread(threadFunc);//Создается новый поток, в котором выполняется функция ThreadFunc
            t.Start();//Запускается
           
        }
        private void threadFunc()//Функция потока
        {
            Ping ping = new Ping();//Создается новый объект ping
            ping.PingCompleted += new PingCompletedEventHandler(Callback);//Добавляется обработчик
            ping.SendAsync("77.88.55.77", 5000);//Происходит асинхронный вызов
        }
        String toOut;
        public delegate void InvokeDelegate();
        public void Callback(Object sender, PingCompletedEventArgs e)//Обработчик
        {
            if (!e.Cancelled && e.Error == null && e.Reply.Status == IPStatus.Success)//Проверяется, успешно ли происходит ping
            {
                toOut = e.Reply.RoundtripTime.ToString();//Запоминается время пинга
                Invoke(new InvokeDelegate(printResult));//Происходит вызов показа результатов в основном потоке
            }
        }
        
        private void printResult()//Показ результатов
        {
            label1.Text = toOut;
        }
    }
}
