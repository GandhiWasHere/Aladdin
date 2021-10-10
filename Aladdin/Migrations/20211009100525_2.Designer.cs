﻿// <auto-generated />
using Aladdin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aladdin.Migrations
{
    [DbContext(typeof(AladdinContext))]
    [Migration("20211009100525_2")]
    partial class _2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aladdin.Models.Cart", b =>
                {
                    b.Property<int>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartTotal")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.HasKey("CartID");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Aladdin.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartID")
                        .HasColumnType("int");

                    b.Property<string>("CustomerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Aladdin.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantityL")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantityM")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantityS")
                        .HasColumnType("int");

                    b.Property<int>("ProductRating")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Aladdin.Models.ProductInCart", b =>
                {
                    b.Property<int>("ProductInCartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantityL")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantityM")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantityS")
                        .HasColumnType("int");

                    b.Property<int>("ProductRating")
                        .HasColumnType("int");

                    b.HasKey("ProductInCartID");

                    b.ToTable("ProductInCart");
                });

            modelBuilder.Entity("Aladdin.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SupplierName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierPhonNumber")
                        .HasColumnType("int");

                    b.HasKey("SupplierID");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("CartProductInCart", b =>
                {
                    b.Property<int>("CartProductsProductInCartID")
                        .HasColumnType("int");

                    b.Property<int>("ProductCartsCartID")
                        .HasColumnType("int");

                    b.HasKey("CartProductsProductInCartID", "ProductCartsCartID");

                    b.HasIndex("ProductCartsCartID");

                    b.ToTable("CartProductInCart");
                });

            modelBuilder.Entity("Aladdin.Models.Product", b =>
                {
                    b.HasOne("Aladdin.Models.Supplier", null)
                        .WithMany("SupplierProducts")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CartProductInCart", b =>
                {
                    b.HasOne("Aladdin.Models.ProductInCart", null)
                        .WithMany()
                        .HasForeignKey("CartProductsProductInCartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aladdin.Models.Cart", null)
                        .WithMany()
                        .HasForeignKey("ProductCartsCartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aladdin.Models.Supplier", b =>
                {
                    b.Navigation("SupplierProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
