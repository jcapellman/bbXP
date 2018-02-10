﻿// <auto-generated />
using bbxp.lib.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace bbxp.lib.Migrations
{
    [DbContext(typeof(BbxpDbContext))]
    [Migration("20180210193859_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("bbxp.lib.DAL.Objects.Content", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Body");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("PostedByUserID");

                    b.Property<string>("Title");

                    b.Property<string>("URLSafename");

                    b.HasKey("ID");

                    b.ToTable("Content");
                });

            modelBuilder.Entity("bbxp.lib.DAL.Objects.DGT_Archives", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<string>("DateString");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("RelativeURL");

                    b.HasKey("ID");

                    b.ToTable("DGT_Archives");
                });

            modelBuilder.Entity("bbxp.lib.DAL.Objects.DGT_MostFrequentedPages", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<string>("Request");

                    b.HasKey("ID");

                    b.ToTable("DGT_MostFrequentedPages");
                });

            modelBuilder.Entity("bbxp.lib.DAL.Objects.DGT_MostFrequentedPagesHeader", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CurrentAsOf");

                    b.Property<int>("RequestCount");

                    b.HasKey("ID");

                    b.ToTable("DGT_MostFrequentedPagesHeader");
                });

            modelBuilder.Entity("bbxp.lib.DAL.Objects.DGT_Posts", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("RelativeURL");

                    b.Property<string>("SafeTagList");

                    b.Property<string>("TagList");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("DGT_Posts");
                });

            modelBuilder.Entity("bbxp.lib.DAL.Objects.Posts", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Body");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<int>("PostedByUserID");

                    b.Property<string>("Title");

                    b.Property<string>("URLSafename");

                    b.HasKey("ID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("bbxp.lib.DAL.Objects.Requests", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("RequestStr");

                    b.HasKey("ID");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("bbxp.lib.DAL.Objects.Users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("Created");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsConfirmed");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
