﻿// <auto-generated />
using System;
using BlogPlatform.Infrastructure.Repositories.MsSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogPlatform.Infrastructure.Repositories.MsSql.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211212181752_AddMaxLength")]
    partial class AddMaxLength
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.BlogAgregate.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("b_BlogId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit")
                        .HasColumnName("b_Blocked");

                    b.Property<string>("BlogUrl")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("b_BlogUrl");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("b_DateCreated");

                    b.Property<string>("ImageName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("b_ImageName");

                    b.Property<string>("PreviewText")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("b_PreviewText");

                    b.Property<string>("SubTitle")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("b_SubTitle");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("b_Title");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("b_UserId");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit")
                        .HasColumnName("b_Visible");

                    b.HasKey("BlogId");

                    b.HasIndex("BlogUrl")
                        .IsUnique()
                        .HasFilter("[b_BlogUrl] IS NOT NULL");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.BlogAgregate.BlogSearch", b =>
                {
                    b.Property<int>("BlogSearchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("bs_BlogSearchId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId")
                        .HasColumnType("int")
                        .HasColumnName("bs_BlogId");

                    b.Property<string>("FullBlogText")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("bs_FullBlogText");

                    b.HasKey("BlogSearchId");

                    b.HasIndex("BlogId")
                        .IsUnique();

                    b.ToTable("BlogSearches");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.PostAgregate.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("p_PostId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit")
                        .HasColumnName("p_Blocked");

                    b.Property<int>("BlogId")
                        .HasColumnType("int")
                        .HasColumnName("p_BlogId");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("p_DateCreated");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2")
                        .HasColumnName("p_DatePosted");

                    b.Property<string>("FooterContent")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("p_FooterContent");

                    b.Property<string>("HeaderContent")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("p_HeaderContent");

                    b.Property<string>("ImageName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("p_ImageName");

                    b.Property<string>("MainContent")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("p_MainContent");

                    b.Property<string>("PreviewText")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("p_PreviewText");

                    b.Property<string>("SubTitle")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("p_SubTitle");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("p_Title");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit")
                        .HasColumnName("p_Visible");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.PostAgregate.PostSearch", b =>
                {
                    b.Property<int>("PostSearchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ps_PostSearchId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullPostText")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ps_FullPostText");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("ps_PostId");

                    b.HasKey("PostSearchId");

                    b.HasIndex("PostId")
                        .IsUnique();

                    b.ToTable("PostSearches");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.UserAgregate.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("u_Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthResourceUserId")
                        .HasColumnType("int")
                        .HasColumnName("u_AuthResourceUserId");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("u_BirthDate");

                    b.Property<int>("BlogId")
                        .HasColumnType("int")
                        .HasColumnName("u_BlogId");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("u_Email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("u_FirstName");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("u_Gender");

                    b.Property<string>("ImageName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("u_ImageName");

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("u_IsVerified");

                    b.Property<string>("LastName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("u_LastName");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("u_PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("AuthResourceUserId")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[u_Email] IS NOT NULL");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[u_PhoneNumber] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.BlogAgregate.Blog", b =>
                {
                    b.HasOne("BlogPlatform.Domain.AgregatesModel.UserAgregate.User", "User")
                        .WithOne("Blog")
                        .HasForeignKey("BlogPlatform.Domain.AgregatesModel.BlogAgregate.Blog", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.BlogAgregate.BlogSearch", b =>
                {
                    b.HasOne("BlogPlatform.Domain.AgregatesModel.BlogAgregate.Blog", "Blog")
                        .WithOne("BlogSearch")
                        .HasForeignKey("BlogPlatform.Domain.AgregatesModel.BlogAgregate.BlogSearch", "BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.PostAgregate.Post", b =>
                {
                    b.HasOne("BlogPlatform.Domain.AgregatesModel.BlogAgregate.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.PostAgregate.PostSearch", b =>
                {
                    b.HasOne("BlogPlatform.Domain.AgregatesModel.PostAgregate.Post", "Post")
                        .WithOne("PostSearch")
                        .HasForeignKey("BlogPlatform.Domain.AgregatesModel.PostAgregate.PostSearch", "PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.BlogAgregate.Blog", b =>
                {
                    b.Navigation("BlogSearch");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.PostAgregate.Post", b =>
                {
                    b.Navigation("PostSearch");
                });

            modelBuilder.Entity("BlogPlatform.Domain.AgregatesModel.UserAgregate.User", b =>
                {
                    b.Navigation("Blog");
                });
#pragma warning restore 612, 618
        }
    }
}
