using GalaSoft.MvvmLight.Command;
using PyaterochkaSimpleSystem.Enums;
using PyaterochkaSimpleSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PyaterochkaSimpleSystem.ViewModels
{
    internal class ItemDialogVM : ViewModelBase
    {
        // Fields
        private string _pageTitle;
        private string _name;
        private string _description;
        private int _amount;
        private bool _isProductPropVisible;
        private string _selectedDate;

        public event EventHandler<ItemDialogData>? DialogClosing; // item or category

        public ItemDialogVM(ItemDialogType type, ItemDialogMode mode)
        {
            _pageTitle = string.Empty;
            _name = string.Empty;
            _description = string.Empty;
            OkButtonCommand = new RelayCommand(OkButtonClickHandler);
            CancelButtonCommand = new RelayCommand(CancelButtonClickHandler);
            ShowCorrectProperties(type, mode);
        }

        // Parameters
        public ICommand OkButtonCommand { get; }
        public ICommand CancelButtonCommand { get; }

        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                _pageTitle = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public bool IsProductPropVisible
        {
            get => _isProductPropVisible;
            set
            {
                _isProductPropVisible = value;
                OnPropertyChanged();
            }
        }

        public string SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                //MessageBox.Show(_selectedDate.ToString());
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        // Methods
        private void OkButtonClickHandler()
        {
            
            if (DateTime.TryParse(SelectedDate, out DateTime dateTime))
            {
                DateOnly _tempDate = DateOnly.FromDateTime(dateTime);
                var data = new ItemDialogData
                {
                    Name = _name,
                    Description = _description,
                    Amount = _amount,
                    SelectedDate = _tempDate,
                };
                DialogClosing?.Invoke(this, data);
            }

            else
            {
                MessageBox.Show($"Некорректное значение даты: {SelectedDate}");
                DialogClosing?.Invoke(this, null);
            }
        }

        private void CancelButtonClickHandler()
        {
            DialogClosing?.Invoke(this, null);
        }

        private void ShowCorrectProperties(ItemDialogType type, ItemDialogMode mode)
        {
            if (type == ItemDialogType.Product)
            {
                IsProductPropVisible = true;
                if (mode == ItemDialogMode.Insert)
                    PageTitle = "Добавление товара";
                if (mode == ItemDialogMode.Update)
                    PageTitle = "Обновление товара";
            }
            else
            {
                IsProductPropVisible = false;
                if (mode == ItemDialogMode.Insert)
                    PageTitle = "Добавление категории";
                if (mode == ItemDialogMode.Update)
                    PageTitle = "Обновление категории";
            }
        }
    }
}

