using System;
using System.Collections.Generic;

public interface IPayment
{
    void ProcessPayment(double amount);
}

public class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing credit card payment of {amount:C}");
    }
}

public class PayPalPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing PayPal payment of {amount:C}");
    }
}

public class BankTransferPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing bank transfer payment of {amount:C}");
    }
}

public interface IDelivery
{
    void DeliverOrder(Order order);
}

public class CourierDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Delivering order {order.OrderId} by courier.");
    }
}

public class PostDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Delivering order {order.OrderId} by post.");
    }
}

public class PickUpPointDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Order {order.OrderId} is ready for pickup.");
    }
}

public interface INotification
{
    void SendNotification(string message);
}

public class EmailNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Sending email notification: {message}");
    }
}

public class SmsNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Sending SMS notification: {message}");
    }
}

public class DiscountCalculator
{
    public double CalculateDiscount(double totalAmount, double discountPercentage)
    {
        return totalAmount * discountPercentage / 100;
    }
}

public class Order
{
    public string OrderId { get; }
    public List<string> Items { get; }
    public double TotalAmount { get; private set; }

    public IPayment PaymentMethod { get; set; }
    public IDelivery DeliveryMethod { get; set; }
    public INotification NotificationMethod { get; set; }

    public Order(string orderId)
    {
        OrderId = orderId;
        Items = new List<string>();
        TotalAmount = 0;
    }

    public void AddItem(string item, double price)
    {
        Items.Add(item);
        TotalAmount += price;
    }

    public void Checkout()
    {
        double discount = new DiscountCalculator().CalculateDiscount(TotalAmount, 10);
        TotalAmount -= discount;
        PaymentMethod.ProcessPayment(TotalAmount);
        DeliveryMethod.DeliverOrder(this);
        NotificationMethod.SendNotification($"Order {OrderId} has been placed and will be delivered.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Order order = new Order("12345");
        order.AddItem("Ticket A", 100);
        order.AddItem("Ticket B", 150);
        order.PaymentMethod = new CreditCardPayment();
        order.DeliveryMethod = new CourierDelivery();
        order.NotificationMethod = new EmailNotification();
        order.Checkout();
    }
}
