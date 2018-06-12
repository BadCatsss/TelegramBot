using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBot
{

    /// <summary>
    /// порядок выполнения
    /// 1 BotClient_OnMessage
    /// 2 MethodCase()
    /// 3  Find_Path()
    /// 4 Создание Grid ( генерация сетки для лабиринта)
    /// 5 Cell_Draw в отдельном потоке
    /// 6  StartGame()
    /// 7   Draw()
    /// 7.1  switch (Image_Name_List[i])
    /// 7.2   Check_ArrayIndex(i)
    /// 8  goto D_Index
    /// 8.1   switch (d)
    /// 8.2 Go_To_Method(d)
    /// </summary>

    public enum neighbour : int //
    {
        west = 0,
        east = 1,
        north = 2,
        south = 3
    }



    public delegate void CreateRow();
    public delegate void CreateColumn();
  
    class Program
    {

        static TelegramBotClient botClient;// создаем клиент
        public static Grid gr;//сетка для лабиринта
   
        public static int CountKey;// состояние зависящие от команды(поочередность посылаемых пользователем команд)
        static void Main(string[] args)
        {
            
            botClient = new TelegramBotClient("yor token");//token
            var BotInformation = botClient.GetMeAsync().Result;
            botClient.StartReceiving();//начать принимать запросы  
            botClient.OnMessage += BotClient_OnMessage;//метод срабатывающий при отправке сообщения
            
           
          
            Console.ReadLine();
            //botClient.StopReceiving();
            CountKey = 0;// первичное состояние после запуска
        }

        public static MessageEventArgs args;
        public static Message My_messege;//текущее сообщение(поле для хранения)
       public static MessageEventArgs arg;//текущее сообщение

        private async static void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
         arg =   Program. args = e;
            await MethodCase();// метод выбора команд
           

        }

        private static  Task MethodCase() // метод выбора команд
        {
            switch (arg.Message.Text)// в зависимости от сообщения
            {
                case "/generate":
                    My_messege = arg.Message;
                    Find_Path();//генерация лабиринта CountKey=1

                    break;
                case "/draw":
                    My_messege = arg.Message;
                    StartGame();
                    break;
                default://если ни один из вариантов
                    My_messege = arg.Message;
                    break;
                    
            }
            return null;
        }

        public static int K;

        public static void StartGame()//начать слать изображения и варианты хода - игроку ПОСЛЕ гинерации лабиринта
        {
            if (CountKey > 0 && Image_Name_List.Capacity > 1)// проверяем был ли вызван  Find_Path() с помощью значения CountKey и сгенерирован ли список имен изображенийдля лабиринта
            {
                Draw();
            }
        }

        public static string image = "";//имя для изображения // инициализация
        public static int LastID;// поле для Go_To_Method
        public static string d = null;//   D_Index

        public static void Check_ArrayIndex(int i) //Check_ArrayIndex(i)- что бы не выйти за пределы списка имен изображений
        {
            if (i < Image_Name_List.Count)// еще не вышли за список доступных имен 
            {
                d = Image_Name_List[i]; // предпроход что бы знать варианты для пользователя - получаем имя изображения по текущему индексу
            }
            else
            {
                d = Image_Name_List[i]; // предпроход что бы знать варианты для пользователя
            }
        }
        public static int C;
        static async public void Draw()// прислать изображение после гинерации 
        {
            
            for (int i = 0; i <= Image_Name_List.Count % 14; i++)
            {

                //if (K==0)
                //{

                //}
                switch (Image_Name_List[i])//выбрать из имен доступных в списке
                {

                    case "1":

                        //var fileToSend1 = new FileToSend("https://drive.google.com/open?id=1QOPNFfTRhJ4z3CggITCoN0W9ScS2fvxp");
                        //botClient.SendPhotoAsync(My_messege.Chat.Id, fileToSend1);// два пути  - лево - право

                        Check_ArrayIndex(i);//  Check_ArrayIndex(i)- что бы не выйти за пределы списка
                        goto D_Index; // предпроход что бы знать варианты для пользователя 



                        break;

                    case "2":
                        //var fileToSend2 = new FileToSend("https://drive.google.com/open?id=1ibjWw820e_9RXGflIRi7SNyu3jjhF64_");
                        //botClient.SendPhotoAsync(My_messege.From.Id, fileToSend2);//один путь - назад

                        Check_ArrayIndex(i); // Check_ArrayIndex(i)- что бы не выйти за пределы списка 
                        goto D_Index;// предпроход что бы знать варианты для пользователя 

                        break;

                    case "3":

                        //var fileToSend3 = new FileToSend("https://drive.google.com/open?id=19JpZL58MmChcAuQocsvTg_wIE1wXxaig");
                        //botClient.SendPhotoAsync(My_messege.From.Id, fileToSend3);// два пути  - лево - право
                        Check_ArrayIndex(i); //Check_ArrayIndex(i) - что бы не выйти за пределы списка

                        goto D_Index;// предпроход что бы знать варианты для пользователя 
                        break;

                    case "4":
                        //var fileToSend4 = new FileToSend("https://drive.google.com/open?id=1KW02avRIb2pfc3GX80sL-Aci8A2xlnBK");
                        //botClient.SendPhotoAsync(My_messege.From.Id, fileToSend4);// два пути  - лево - право

                        Check_ArrayIndex(i); //  Check_ArrayIndex(i)- что бы не выйти за пределы списка
                        goto D_Index;// предпроход что бы знать варианты для пользователя 
                        break;

                    case "5":
                        //var fileToSend5 = new FileToSend("https://drive.google.com/open?id=13ipoicH8DpWQn2EP3p0oD3FtFzx42Tpu");
                        //botClient.SendPhotoAsync(My_messege.From.Id, fileToSend5);// два пути  - лево - право

                        Check_ArrayIndex(i);  //  Check_ArrayIndex(i)- что бы не выйти за пределы списка
                        goto D_Index;// предпроход что бы знать варианты для пользователя
                        break;

                }
               
        D_Index: // предпроход что бы знать варианты для пользователя
                switch (d)// d получаем в Check_ArrayIndex
                {
                    case "1":
                        C = 0;
                        await Go_To_Method(d); // предпроход что бы знать варианты для пользователя(следующие варианты шагов в лабиринте и соответствующие изображения)

                        break;
                    case "2":
                        C = 0;
                        await Go_To_Method(d);// предпроход что бы знать варианты для пользователя(следующие варианты шагов в лабиринте и соответствующие изображения)
                        break;
                    case "3":
                        await Go_To_Method(d);// предпроход что бы знать варианты для пользователя(следующие варианты шагов в лабиринте и соответствующие изображения)
                        break;
                    case "4":
                        C = 0;
                        await Go_To_Method(d); // предпроход что бы знать варианты для пользователя(следующие варианты шагов в лабиринте и соответствующие изображения)
                        break;
                    case "5":
                        C = 0;
                        await Go_To_Method(d);// предпроход что бы знать варианты для пользователя(следующие варианты шагов в лабиринте и соответствующие изображения)
                        break;
                    default:
                        break;
                };
            }


        }
        
        public static string wariant = null;

        public async static Task Go_To_Method(string Id) // предпроход что бы знать варианты для пользователя(следующие варианты шагов в лабиринте и соответствующие изображения)
        {

            LastID = System.Convert.ToInt32(Id);
            //1 - лево - право
            //2 - назад
            //3 - лево право
            //4 - лево право
            //5 -вперед
            var fileToSend1 = new FileToSend("https://drive.google.com/open?id=1QOPNFfTRhJ4z3CggITCoN0W9ScS2fvxp");//изображение
            var fileToSend2 = new FileToSend("https://drive.google.com/open?id=1ibjWw820e_9RXGflIRi7SNyu3jjhF64_");//изображение
            var fileToSend3 = new FileToSend("https://drive.google.com/open?id=19JpZL58MmChcAuQocsvTg_wIE1wXxaig");//изображение
            var fileToSend4 = new FileToSend("https://drive.google.com/open?id=1KW02avRIb2pfc3GX80sL-Aci8A2xlnBK");//изображение
            var fileToSend5 = new FileToSend("https://drive.google.com/open?id=13ipoicH8DpWQn2EP3p0oD3FtFzx42Tpu");//изображение
            var dd = fileToSend3;// переменная в которую добавляем текущее подходящее изображение
            string s = null;
            string s2 = null;

            switch (LastID)
            {
                case 1:
                    dd = fileToSend1;
                    s = "/left";//следующие возможные варианты(варианты для следующего хода)
                    s2 = "/right";//следующие возможные варианты(варианты для следующего хода)
                    break;
                case 3:
                    dd = fileToSend3;
                    s = "/left";//следующие возможные варианты(варианты для следующего хода)
                    s2 = "/right";//следующие возможные варианты(варианты для следующего хода)
                    break;
                case 4:
                    dd = fileToSend4;
                    s = "/left";//следующие возможные варианты(варианты для следующего хода)
                    s2 = "/right";//следующие возможные варианты(варианты для следующего хода)
                    break;
                case 5:
                    dd = fileToSend5;
                    s = "/foward";//следующие возможные варианты(варианты для следующего хода)
                    break;
                default:
                    dd = fileToSend2;
                    s = "/back";//следующие возможные варианты(варианты для следующего хода)
                    break;
            };
            if (C == 0)
            {
                await botClient.SendPhotoAsync(My_messege.Chat.Id, dd);//отсылаем изображение
                await botClient.SendTextMessageAsync(My_messege.Chat.Id, s + " " + s2);//отсылаем следующие возможные варианты(варианты для следующего хода)
                Program.BotClient_OnMessage(botClient, args);// там содержится MethodCase (перевызов MethodCase)
                wariant = args.Message.Text;
                C++;
               
                 

                return;
            }

            if (wariant == "/left" && (LastID == 1 || LastID == 3 || LastID == 4))// то что выбрал пользователь(wariant) и совподающий с ним  LastID
            {
                s = "/left";
                s2 = "/right";
                await botClient.SendPhotoAsync(My_messege.Chat.Id, dd);//ОТОСЛАТЬ СЛЕДУЮЩЕЕ ИЗОБРАЖЕНИЕ: два пути  - лево - право
                goto check;
            }
            if (wariant == "/right" && LastID == 1 || LastID == 3 || LastID == 4)
            {
                s = "/right";
                s2 = "/left";
                await botClient.SendPhotoAsync(My_messege.Chat.Id, dd);//ОТОСЛАТЬ СЛЕДУЮЩЕЕ ИЗОБРАЖЕНИЕ: два пути  - лево - право
            }

            if (wariant == "/back" && LastID == 2)
            {
                s = "/back";
                await botClient.SendPhotoAsync(My_messege.From.Id, fileToSend2);//ОТОСЛАТЬ СЛЕДУЮЩЕЕ ИЗОБРАЖЕНИЕ:один путь - назад
            }

            if (wariant == "/foward" && LastID == 5)//ОТОСЛАТЬ СЛЕДУЮЩЕЕ ИЗОБРАЖЕНИЕ: один путь - вперед
            {
                s = "/forward";
                await botClient.SendPhotoAsync(My_messege.From.Id, fileToSend5);
            }

            check:
           wariant = args.Message.Text;
            await botClient.SendTextMessageAsync(My_messege.Chat.Id, s + " " + s2);//ОТОСЛАТЬ СЛЕДУЮЩЕЕ ИЗОБРАЖЕНИЕ: два пути  - лево - право

        }
        public static List<string> Image_Name_List;//список доступных имен изображений, которые добавляются на основании пути в лабиринте

        public static int rnd;
        public static Thread Main_Thread;

        public static async void Find_Path()//генерация лабиринта
        {
            Grid gr = new Grid(3, 3);
            CountKey = 1;
            Random random = new Random();
            rnd = random.Next(0, 2);//случайные варианты для генерации пути (направления)
            Console.WriteLine("its done!");
            Thread thread2 = new Thread(Cell_Draw);//ГЕНЕРИРУЕТ ПУТЬ ДЛЯ ИГРОКА;// в отдельном потоке - для будущей возможности предпрохода в Go_To_Method
            thread2.Start();
            Main_Thread = Thread.CurrentThread;
            Main_Thread.Suspend();


        }

        public static void Cell_Draw()// ОТВЕЧАЕТ ЗА СОСТАВЛЕНИЕ  СПИСКА ИМЕН ИЗОБРАЖЕНИЙ И Генерации пути для игрока в созданном лабиринте
        {


            Image_Name_List = new List<string>();// составляем список доступных имен изображений
            var d = 0;
            for (int i = 1; i < (Column.Horizontal_wall.Count + Column.Vertical_wall.Count) / 2; i++)//количество ячеек
            {


                for (int j = i; j >= d; j++)//находим ячейку
                {
                    if (rnd == 0)
                    {
                        Column.Vertical_wall[i] = false;// прорубаем стены в выбранном направлении
                        image = "1";//добавляем название подходящего  изображения
                        Image_Name_List.Add(image);//добавляем в спсиок возможных изображений(добавляем название для поиска по нему)
                        j++;
                        d = Column.cellArr[System.Convert.ToInt32(Math.Log(System.Convert.ToDouble(i)))].index;
                        goto flag;// ограничение по памяти
                    }

                    if (rnd == 1)
                    {
                        Column.Horizontal_wall[i] = false;
                        image = "3";
                        Image_Name_List.Add(image);
                        //StartGame();
                        j++;
                        d = Column.cellArr[System.Convert.ToInt32(Math.Log(System.Convert.ToDouble(i)))].index;
                        goto flag;
                    }

                    if (rnd == 2)
                    {
                        Column.Horizontal_wall[i - 1] = false;
                        image = "4";
                        Image_Name_List.Add(image);
                        //StartGame();
                        j++;
                        d = Column.cellArr[System.Convert.ToInt32(Math.Log(System.Convert.ToDouble(i)))].index;
                        goto flag;
                    }

                    else
                    {
                        image = "2";
                        Image_Name_List.Add(image);
                        //StartGame();
                        j++;
                        d = Column.cellArr[System.Convert.ToInt32(Math.Log(System.Convert.ToDouble(i)))].index;
                        goto flag;
                    }

                }
                flag:;


            }
            //Main_Thread.Resume();
            //StartGame();
        }
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////// генерация сетки для лабиринта
    /// </summary>
    public class Grid
    {
        public static List<Row> rL;//спсиок строк

        public Grid(int row, int column)//конструктор
        {


            rL = new List<Row>(row);//список строк
            for (int i = 0; i < row; i++)//создаем строки
            {
                rL.Add(new Row(i, column));//создать строку по индексу
            }


            for (int i = 0; i < Cell.east.Capacity; i++)
            {

                for (int j = 0; j < Cell.south.Capacity; j++)
                {
                    if (Cell.west[i] == true || Cell.east[i] == true || Cell.north[i] == true || Cell.south[i] == true)
                    {

                        Column.Vertical_wall.Add(true);
                    }

                    if (Cell.west[j] == true || Cell.east[j] == true || Cell.north[j] == true || Cell.south[j] == true)
                    {
                        Column.Horizontal_wall.Add(true);
                    }
                }


            }


        }


    }

    public class Row
    {
        public int row;
        public static event CreateRow onRowCreate;// при создании строки
        public static List<Column> cL = new List<Column>();
        public Row(int row, int column)//конструктор
        {
            onRowCreate += new CreateRow(Cell.Re_Linked_atRow);//(класс Cell) // поиск границ соседей лабиринта 
            this.row = row;

            for (int i = 0; i < Grid.rL.Capacity; i++)//пока не привысили количество заданных строк
            {
                for (int j = 0; j < column; j++)//пока не привысили количество заданных столбцов
                {
                    cL.Add(new Column(column));// создаем столбцы (см  class Column)
                }

            }
            onRowCreate.Invoke();
        }




    }

    public class Cell //ячейка
    {


        public static List<bool> west = new List<bool>();//список соседей
        public static List<bool> east = new List<bool>();//список соседей
        public static List<bool> north = new List<bool>();//список соседей
        public static List<bool> south = new List<bool>();//список соседей
        static int RowCount = 0;
        static int ColumnCount = 0;
        int Cell_Index;


        public Cell(int Index)
        {
            this.Cell_Index = Index;
        }

        public int index
        {
            get { return Cell_Index; }
        }



        public static void Re_Linked_atRow()
        {
            if (RowCount == 0)// поиск границ лабиринта
            {
                south.Add(true);
                north.Add(false);
            }

            if (ColumnCount == 0)// поиск границ лабиринта
            {
                west.Add(true);
                east.Add(false);
            }
            else
            {
                south.Add(true);
                north.Add(true);
            }

            if (north.Capacity == Grid.rL.Capacity)// если количество соседей равно заданному количеств строк
            {
                north.Add(false);//больше не добавляем соседей
            }

            if (west.Capacity == Row.cL.Capacity)// если количество соседей равно заданному количеств строк
            {
                west.Add(false);//больше не добавляем соседей
            }

        }
        public static void Re_Linked_atColumn()
        {
            if (ColumnCount == 0)// поиск границ лабиринта
            {
                west.Add(true);
            }
            else
            {
                west.Add(true);// добавление соседей
                east.Add(true);// добавление соседей
            }
            int c = Column.Counter;
            Column.Counter = c;


            // var dARR = Column.cellArr;//временный массив
            // int lengthD = dARR.Length;
            //Column. cellArr = new Cell[Column. Counter+lengthD];
            // for (int i = 0; i < lengthD; i++)
            // {
            //     Column.cellArr[i] = dARR[i];
            // }

        }



    }

    public class Column//столбцы
    {
        public static List<bool> Vertical_wall = new List<bool>();//список стен
        public static List<bool> Horizontal_wall = new List<bool>();//список стен


        public static List<Cell> cellArr = new List<Cell>();//список  ячеек

        public static event CreateColumn onColumnCreate;// событие на создание столбца
        //public static int Cell_Counter=0;
        int column;
        public static int Counter;//счетчик столбцов


        public Column(int column)//конструктор
        {
            this.column = column;
            Counter = column;//количество столбцов
            onColumnCreate += new CreateColumn(Cell.Re_Linked_atColumn);// поиск границ лабиринта / добавление соседей (класс Cell)
            onColumnCreate.Invoke();//вызов события

            for (int i = 0; i < Counter; i++)
            {
                cellArr.Add(new Cell(i));//добавляем ячейки по количеству столбцов
                //Cell_Counter++;
            }


        }


    }
}
