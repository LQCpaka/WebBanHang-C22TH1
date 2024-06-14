using NUnit.Framework;
using System.Net.Http.Headers;


namespace TestCart
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        //[Test]
        //public void Can_Add_To_Cart()
        //{
        //    Product p1 = new Product { Id = 1, Name = "A" };
        //    Product p2 = new Product { Id = 2, Name = "B" };
        //    Cart cart = new Cart();
        //    cart.Add(p1, 1);
        //    cart.Add(p2, 1);

        //    CartItem[] _items = cart.Items.ToArray;
        //    Assert.AreEqual(3, _items.Length);
        //    Assert.AreEqual(p1, _items[0].Product);
        //    Assert.AreEqual(p2, _items[1].Product);
        //    Assert.AreEqual(p3, _items[1].Product);



        //}
        //[Test]
        //public void Can_Add_To_Cart()
        //{
        //    Product p1 = new Product { Id = 1, Name = "A" };
        //    Product p2 = new Product { Id = 2, Name = "B" };
        //    Cart cart = new Cart();
        //    cart.Add(p1, 1);
        //    cart.Add(p2, 1);

        //    CartItem[] _items = cart.Items.ToArray;
        //    Assert.AreEqual(3, _items.Length);
        //    Assert.AreEqual(p1, _items[0].Product);
        //    Assert.AreEqual(p2, _items[1].Product);
        //    Assert.AreEqual(p3, _items[1].Product);



        //}
    }
}