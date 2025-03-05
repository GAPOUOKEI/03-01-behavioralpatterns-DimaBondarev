using System;

namespace OrderProcessingSystem
{
    // Бондарев Дима 3пк2 Вар 5, задание 2:Спроектируйте систему обработки заказов с состояниями, такими как "Новый заказ", "В обработке",
    // "Отправлено", "Доставлено", "Отменено". Изменение состояния должно определять доступные действия.
    public enum OrderStateType
    {
        Новый,
        ВОбработке,
        Отправлено,
        Доставлено,
        Отменено
    }

    public class Order
    {
        private IOrderState _state;

        public Order()
        {
            _state = new NewOrderState(this);
        }

        public void SetState(IOrderState state)
        {
            _state = state;
            Console.WriteLine($"Состояние заказа изменено на: {_state.GetType().Name}");
        }

        public void Process() => _state.Process();
        public void Ship() => _state.Ship();
        public void Deliver() => _state.Deliver();
        public void Cancel() => _state.Cancel();
    }

    public interface IOrderState
    {
        void Process();
        void Ship();
        void Deliver();
        void Cancel();
    }

    public class NewOrderState : IOrderState
    {
        private readonly Order _order;
        public NewOrderState(Order order) => _order = order;

        public void Process() => _order.SetState(new ProcessingOrderState(_order));
        public void Ship() => Console.WriteLine("Нельзя отправить новый заказ. Сначала обработайте его.");
        public void Deliver() => Console.WriteLine("Нельзя доставить новый заказ.");
        public void Cancel() => _order.SetState(new CanceledOrderState(_order));
    }

    public class ProcessingOrderState : IOrderState
    {
        private readonly Order _order;
        public ProcessingOrderState(Order order) => _order = order;

        public void Process() => Console.WriteLine("Заказ уже в обработке.");
        public void Ship() => _order.SetState(new ShippedOrderState(_order));
        public void Deliver() => Console.WriteLine("Нельзя доставить. Заказ ещё не отправлен.");
        public void Cancel() => _order.SetState(new CanceledOrderState(_order));
    }

    public class ShippedOrderState : IOrderState
    {
        private readonly Order _order;
        public ShippedOrderState(Order order) => _order = order;

        public void Process() => Console.WriteLine("Нельзя обработать. Заказ уже отправлен.");
        public void Ship() => Console.WriteLine("Заказ уже отправлен.");
        public void Deliver() => _order.SetState(new DeliveredOrderState(_order));
        public void Cancel() => Console.WriteLine("Нельзя отменить. Заказ уже отправлен.");
    }

    public class DeliveredOrderState : IOrderState
    {
        private readonly Order _order;
        public DeliveredOrderState(Order order) => _order = order;

        public void Process() => Console.WriteLine("Заказ уже доставлен.");
        public void Ship() => Console.WriteLine("Заказ уже доставлен.");
        public void Deliver() => Console.WriteLine("Заказ уже доставлен.");
        public void Cancel() => Console.WriteLine("Нельзя отменить доставленный заказ.");
    }

    public class CanceledOrderState : IOrderState
    {
        private readonly Order _order;
        public CanceledOrderState(Order order) => _order = order;

        public void Process() => Console.WriteLine("Нельзя обработать. Заказ отменён.");
        public void Ship() => Console.WriteLine("Нельзя отправить. Заказ отменён.");
        public void Deliver() => Console.WriteLine("Нельзя доставить. Заказ отменён.");
        public void Cancel() => Console.WriteLine("Заказ уже отменён.");
    }

    class Program
    {
        static void Main()
        {
            Order order = new Order();

            order.Process();
            order.Ship();
            order.Deliver();
            order.Cancel();
        }
    }
}
