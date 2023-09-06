using System.Data;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Npgsql;

namespace Otus
{
    internal class ShopRepository
    {
        private readonly string _connectionString;

        public ShopRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Customer>? GetCustomersByFirstName(string firstName)
        {
            IEnumerable<Customer>? customers;
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                customers = db.Query<Customer>(@"SELECT * FROM ""Customers"" WHERE ""FirstName"" = @firstName", new { firstName });
            }
            return customers;
        }

        public IEnumerable<Customer>? GetCustomersByLastName(string lastName)
        {
            IEnumerable<Customer>? customers;
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                customers = db.Query<Customer>(@"SELECT * FROM ""Customers"" WHERE ""LastName"" = @lastName", new { lastName });
            }
            return customers;
        }

        public IEnumerable<Customer>? GetCustomersByAge(int age)
        {
            IEnumerable<Customer>? customers;
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                customers = db.Query<Customer>(@"SELECT * FROM ""Customers"" WHERE ""Age"" = @age", new { age });
            }
            return customers;
        }

        public IEnumerable<Customer>? GetShopInfo(int idProduct, int age)
        {
            using IDbConnection db = new NpgsqlConnection(_connectionString);

            var query = db.Query<Customer, Order, Product, Customer>(@"
                    SELECT 
                        *
                    FROM ""Customers"" as c
                    JOIN ""Orders"" as o ON c.""Id""=o.""CustomersId""
                    JOIN ""Products"" as p ON o.""ProductId""=p.""Id""
                    WHERE o.""ProductId""=@idProduct AND c.""Age"">@age",
            (customer, order, product) =>
            {
                customer.Products.Add(product);
                customer.Orders.Add(order);
                order.Customer = customer;
                order.Product = product;

                return customer;
            }, new { idProduct, age });

            var customers = query.GroupBy(x => x.Id).Select(x => 
            {
                var group = x.First();
                group.Products = x.Select(c => c.Products.Single()).ToList();
                group.Orders = x.Select(c => c.Orders.Single()).ToList();
                return group;
            });
            return customers;
        }
    }
}