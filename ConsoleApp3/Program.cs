using System;
using System.Collections.Generic;

namespace ShoppingCartSimulation
{
    // Бондарев Дима 3пк2 Вар 5, задание 3:Реализуйте симуляцию магазина, где пользователи могут выполнять команды
    // "Добавить товар в корзину", "Удалить товар из корзины" и "Оформить заказ" с возможностью отмены операций.
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class ShoppingCart
    {
        private List<string> _items = new List<string>();

        public void AddItem(string item)
        {
            _items.Add(item);
            Console.WriteLine($"Добавлен товар: {item}");
        }

        public void RemoveItem(string item)
        {
            if (_items.Remove(item))
                Console.WriteLine($"Удален товар: {item}");
            else
                Console.WriteLine($"Товар {item} не найден в корзине");
        }

        public void Checkout()
        {
            if (_items.Count > 0)
            {
                Console.WriteLine("Заказ оформлен!");
                _items.Clear();
            }
            else
            {
                Console.WriteLine("Корзина пуста!");
            }
        }
    }

    public class AddItemCommand : ICommand
    {
        private ShoppingCart _cart;
        private string _item;

        public AddItemCommand(ShoppingCart cart, string item)
        {
            _cart = cart;
            _item = item;
        }

        public void Execute()
        {
            _cart.AddItem(_item);
        }

        public void Undo()
        {
            _cart.RemoveItem(_item);
        }
    }

    public class RemoveItemCommand : ICommand
    {
        private ShoppingCart _cart;
        private string _item;

        public RemoveItemCommand(ShoppingCart cart, string item)
        {
            _cart = cart;
            _item = item;
        }

        public void Execute()
        {
            _cart.RemoveItem(_item);
        }

        public void Undo()
        {
            _cart.AddItem(_item);
        }
    }

    public class CheckoutCommand : ICommand
    {
        private ShoppingCart _cart;

        public CheckoutCommand(ShoppingCart cart)
        {
            _cart = cart;
        }

        public void Execute()
        {
            _cart.Checkout();
        }

        public void Undo()
        {
            Console.WriteLine("Оформление заказа нельзя отменить!");
        }
    }

    public class CommandManager
    {
        private Stack<ICommand> _commandHistory = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commandHistory.Push(command);
        }

        public void UndoLastCommand()
        {
            if (_commandHistory.Count > 0)
            {
                var lastCommand = _commandHistory.Pop();
                lastCommand.Undo();
            }
            else
            {
                Console.WriteLine("Нет команд для отмены");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            ShoppingCart cart = new ShoppingCart();
            CommandManager commandManager = new CommandManager();

            commandManager.ExecuteCommand(new AddItemCommand(cart, "Ноутбук"));
            commandManager.ExecuteCommand(new AddItemCommand(cart, "Смартфон"));
            commandManager.UndoLastCommand();
            commandManager.ExecuteCommand(new CheckoutCommand(cart));
            commandManager.UndoLastCommand();
        }
    }
}