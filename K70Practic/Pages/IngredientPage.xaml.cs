using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace K70Practic.Pages
{
    /// <summary>
    /// Логика взаимодействия для IngredientPage.xaml
    /// </summary>
    public partial class IngredientPage : Page
    {
        RecieptsDBEntities context;
        public IngredientPage()
        {
            InitializeComponent();
            //подключение к БД
            context = new RecieptsDBEntities();
            //закинуть все данные из таблицы Ингредиенты в таблицу
            ingredientTable.ItemsSource = context.Ingredient.ToList();
            //закинуть данные из таблицы unit в комбобокс
            unitBox.ItemsSource = context.Unit.ToList();


            //Добавление
            Unit unit = new Unit();
            unit.Id = 4;
            unit.Name = "Литры";
            context.Unit.Add(unit);
            context.SaveChanges();

            //Редактирование
            Unit unit1 = context.Unit.Find(4);
            unit1.Name = "Килограммы";
            context.SaveChanges();


            //удаление
            Unit unit2 = context.Unit.Find(4);
            context.Unit.Remove(unit2);
            context.SaveChanges();

        }


        //фильтрация данных таблицы
        void ChangeData()
        {
            //получаем объект Unit из комбобокса
            Unit unit = unitBox.SelectedItem as Unit;

            //создаем список всех ингредиентов
            var list = context.Ingredient.ToList();
            //если выбран какой-то элемент
            if (unit != null)
            {
                //в лист записать только те строки, у которых idЕд.Измерения такой как у выбранного в комбобоксе
                 list= list.Where(
                    x => x.UnitId == unit.Id).ToList();
            }


            //если текстовое поле не пустое и не заполнено пробелами
            if (!string.IsNullOrWhiteSpace(searchBox.Text))
            {
                //в список закинуть те строки, у которых имя содержит символы из текстбокса
                list = list.Where(x => x.Name.Contains(searchBox.Text)).ToList();
            }

            //закинуть список в таблицу
            ingredientTable.ItemsSource = list;
        }

        //изменение выбранного в комбобоксе
        private void SelectUnitChange(object sender, SelectionChangedEventArgs e)
        {
            ChangeData();
            
        }

        //изменение текста в текстбоксе
        private void textChange(object sender, TextChangedEventArgs e)
        {
            ChangeData();
        }
    }
}
