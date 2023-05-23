﻿using System;
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
using System.Windows.Controls.Primitives;
using Microsoft.Win32;

namespace WpfPP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class User_window_admin : Window
    {
        private int surname = 0;
        private int name = 0;

        Entities entities = new Entities();
        public User_window_admin()
        {
            InitializeComponent();

            foreach (var entity in entities.User)
                main_list.Items.Add(entity);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (poisk.Text != null)
            {
                Text_Searc.Text = "";
            }
            else
            {
                Text_Searc.Text = "  Search a users";
            }

            if (string.IsNullOrEmpty(poisk.Text.Trim()) == false)
            {
                using (Entities entities = new Entities())
                {
                    main_list.Items.Clear();
                    foreach (var items in entities.User)
                    {
                        if (items.login.ToString().StartsWith(poisk.Text.ToLower()) ||
                            items.password.ToString().StartsWith(poisk.Text.ToLower()))
                            main_list.Items.Add(items);
                    }
                }
            }
            else if (poisk.Text.Trim() == "")
            {
                main_list.Items.Clear();
                foreach (var item in entities.User)
                    main_list.Items.Add(item);
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_product_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "User";
            main_list.SelectedIndex = -1;

            text_login.Text = "";
            text_pass.Text = "";
        }

        private void main_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selected = main_list.SelectedItem as User;
                if(selected != null)
                {
                    Title.Content = selected.login;
                    text_login.Text = selected.login;
                    text_pass.Text = selected.password;

                }
            }
            catch
            {
                Message message = new Message();
                message.Show();
            }
        }

        private void clear_product_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Categories";

            text_login.Text = "";
            text_pass.Text = "";

            main_list.SelectedIndex = -1;
        }

        private void deelte_product_Click(object sender, RoutedEventArgs e)
        {
            var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rezult == MessageBoxResult.No)
                return;
            var delete = main_list.SelectedItem as User;

            if (delete != null)
            {
                entities.User.Remove(delete);
                entities.SaveChanges();

                text_login.Clear();
                text_pass.Clear();

                main_list.Items.Remove(delete);
            }
            else
            {
                Message message = new Message();
                message.Main.Content = "There are no objects to be deleted!";
                message.label.Content = "Error";
                message.Show();
            }
        }

        private void save_product_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var save = main_list.SelectedItem as User;
                if (text_login.Text == "" || text_pass.Text == "")
                {
                    Message message = new Message();
                    message.label.Content = "Mistake";
                    message.Show();
                }
                else
                {
                    if (save == null)
                    {
                        save = new User();
                        entities.User.Add(save);
                        main_list.Items.Add(save);
                    }
                    save.login = text_login.Text;
                    save.password = text_pass.Text;

                    entities.SaveChanges();
                    main_list.Items.Refresh();
                }
            }
            catch
            {
                Message message = new Message();
                message.label.Content = "Select an image!";
                message.Show();
            }
        }

        private void HamburgerMenuItem_Selected_1(object sender, RoutedEventArgs e)
        {
            Client_window_admin authorization = new Client_window_admin();
            authorization.Show();
            Close();
        }

        private void Home_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Home;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Home";
        }

        private void Home_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Сontract_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Profile;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Contract";
        }

        private void Сontract_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Settings;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Сlientele";
        }

        private void Settings_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Exit;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Exit";
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            // null
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }
        private void Сontract_Click(object sender, RoutedEventArgs e)
        {
            Contract_window_admin contract_Window = new Contract_window_admin();
            contract_Window.Show();
            Close();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_Admin window = new MainWindow_Admin();
            window.Show();
            Close();
        }

        private void text_Percent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void surname_client_Click(object sender, RoutedEventArgs e)
        {
            name = 0;

            border_price.Visibility = Visibility.Hidden;

            surname++;
            if (surname % 2 == 1)
            {
                border_title.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name_category", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_title.Visibility = Visibility.Hidden;
                main_list.Items.SortDescriptions.Clear();
            }
        }

        private void name_client_Click(object sender, RoutedEventArgs e)
        {
            surname = 0;

            border_title.Visibility = Visibility.Hidden;

            name++;
            if (name % 2 == 1)
            {
                border_price.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Id_Category", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_price.Visibility = Visibility.Hidden;
                main_list.Items.SortDescriptions.Clear();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Client_window_admin client_Window_Admin = new Client_window_admin();
            client_Window_Admin.Show();
            Close();
        }

        private void category_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = category;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Category";
        }

        private void category_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void category_Click(object sender, RoutedEventArgs e)
        {
            Category_window_admin category_Window_Admin = new Category_window_admin();
            category_Window_Admin.Show();
            Close();
        }

        private void sotrudnik_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = sotrudnik;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Employee";
        }

        private void sotrudnik_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void sotrudnik_Click(object sender, RoutedEventArgs e)
        {
            Sotrudnik_window_admin sotrudnik_Window_Admin = new Sotrudnik_window_admin();
            sotrudnik_Window_Admin.Show();
            Close();
        }

        private void users_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = users;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Users";
        }

        private void users_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void users_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
