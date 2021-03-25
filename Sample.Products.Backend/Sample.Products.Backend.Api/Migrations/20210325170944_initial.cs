using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Products.Backend.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    SeoFilename = table.Column<string>(nullable: true),
                    AltAttribute = table.Column<string>(nullable: true),
                    TitleAttribute = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false),
                    BinaryData = table.Column<byte[]>(nullable: true),
                    VirtualPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Customers_UserId",
                        column: x => x.UserId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Customers_UserId",
                        column: x => x.UserId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Customers_UserId",
                        column: x => x.UserId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: false),
                    PictureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    PictureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_RegisteredRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RegisteredRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_RegisteredRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RegisteredRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Customers_UserId",
                        column: x => x.UserId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    BrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPictures",
                columns: table => new
                {
                    PictureId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPictures", x => new { x.PictureId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductPictures_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPictures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => new { x.TagId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name", "PictureId" },
                values: new object[,]
                {
                    { 1, "cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est la", null },
                    { 74, "laborum.Lorem ipsum", null },
                    { 72, "ut labore et dolore magna aliqua. Ut enim ad minim veniam, qu", null },
                    { 71, "officia deserunt mollit anim id est labor", null },
                    { 70, "Excepteur sint ", null },
                    { 69, "esse c", null },
                    { 68, "Excepteur sint ", null },
                    { 67, "quis nostrud exercitation ullamco laboris nis", null },
                    { 66, "aute irure dolor in ", null },
                    { 65, "sunt in culpa", null },
                    { 64, "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in v", null },
                    { 63, "magna aliqua. Ut enim ad minim venia", null },
                    { 62, "id est laborum.Lorem ipsum dolor", null },
                    { 61, "ipsum dolor", null },
                    { 60, "deserunt mollit anim id est lab", null },
                    { 59, "orum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut la", null },
                    { 58, "ut aliquip ex ea commodo consequat. Duis aute i", null },
                    { 57, "non p", null },
                    { 56, "exercitation ullamco laboris nisi ut aliquip ex ea commodo con", null },
                    { 55, "consectetur adipis", null },
                    { 54, "velit esse cillum dolore eu fugiat nulla pariatur. Excepteur si", null },
                    { 53, "labore et dolore ", null },
                    { 75, "sed do eiusmod tempor incididunt ut labore et dolore ", null },
                    { 76, "Duis aute irure dolor in reprehender", null },
                    { 77, "exercitation ullamco laboris nisi ut aliquip ex ea commodo conse", null },
                    { 78, "id est laborum.Lorem ipsum dolor sit amet, consec", null },
                    { 100, "sint occaecat cupidatat non proident, sunt in ", null },
                    { 99, "deserunt mollit anim id est ", null },
                    { 98, "amet, consectetur adipisci", null },
                    { 97, "qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consect", null },
                    { 96, "sit amet, consectetur adipiscing elit, sed do eiusmod t", null },
                    { 95, "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui ", null },
                    { 94, "dolore eu fugiat nu", null },
                    { 93, "nostrud exerci", null },
                    { 92, "ex ea commodo consequat. Duis aute irure dolor in reprehenderit in volupt", null },
                    { 91, "pariatur. Excepteur sint occaecat cupidatat non proident, su", null },
                    { 52, "id es", null },
                    { 90, "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure", null },
                    { 88, "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in ", null },
                    { 87, "Ut enim ad minim veniam, qui", null },
                    { 86, "conseq", null },
                    { 85, "cillum ", null },
                    { 84, "veniam, quis nostrud exercitati", null },
                    { 83, "commodo consequat. Duis aut", null },
                    { 82, "non proident, sunt in culpa qu", null },
                    { 81, "dolore magna aliqua. Ut enim ad minim veniam,", null },
                    { 80, "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupida", null },
                    { 79, "sit amet, consectetur adipiscing elit, sed", null },
                    { 89, "elit, sed do eiusmod tempor incididunt ut labore et dolor", null },
                    { 51, "nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in", null },
                    { 73, "non proident, sunt in", null },
                    { 49, "deserunt mol", null },
                    { 23, "et dolo", null },
                    { 22, "irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fug", null },
                    { 21, "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui o", null },
                    { 20, "adipis", null },
                    { 19, "cillum dolore eu fugiat nulla pariatur. Ex", null },
                    { 18, "deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisci", null },
                    { 17, "adipiscing elit, sed do eiusmod tempor incididunt ut lab", null },
                    { 16, "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat n", null },
                    { 15, "Duis aute irure dolor in reprehenderit in vo", null },
                    { 14, "sint occaecat cupidatat non ", null },
                    { 24, "magna aliqua. Ut enim ad minim ", null },
                    { 13, "laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor in", null },
                    { 11, "eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, ", null },
                    { 10, "dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore ", null },
                    { 9, "borum.Lorem ipsum dolor sit amet, consec", null },
                    { 8, "et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris ", null },
                    { 7, "minim ", null },
                    { 6, "quis nostrud exercitation ullamco laboris nisi ", null },
                    { 5, "exercitation ullamco l", null },
                    { 50, "id est laborum.Lorem ipsum dolor sit amet, consectet", null },
                    { 3, "anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmo", null },
                    { 2, "ut labore et do", null },
                    { 12, "deseru", null },
                    { 25, "magna", null },
                    { 4, "aute irure dolor in reprehen", null },
                    { 27, "i", null },
                    { 26, "Duis aute irure dolor in repreh", null },
                    { 48, "in culpa qui officia deserunt mollit anim id", null },
                    { 46, "m.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do ei", null },
                    { 45, "culpa qui of", null },
                    { 44, "lab", null },
                    { 43, "cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat no", null },
                    { 42, "ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labo", null },
                    { 41, "enim ad minim veniam, quis nostrud exercitatio", null },
                    { 40, "elit, sed do eiusmod tempor incididunt ut labore e", null },
                    { 39, "Excepteur sint occaecat cupidatat non proident, sunt in culpa qu", null },
                    { 38, "anim id est la", null },
                    { 47, "irure dolor in reprehenderit in voluptat", null },
                    { 36, "eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qu", null },
                    { 35, "adipiscing elit, sed do eiusmod tempor incididunt ut ", null },
                    { 34, "elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, q", null },
                    { 33, "in reprehenderit in voluptate velit", null },
                    { 32, "sint occaecat cupidatat non proident, sunt in culpa qui o", null },
                    { 31, "Excepteur sint occaecat cupidatat non proident, sunt in cul", null },
                    { 30, "quis ", null },
                    { 29, "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore ma", null },
                    { 28, "eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ", null },
                    { 37, "dolor in reprehenderit in volupt", null }
                });

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "AltAttribute", "BinaryData", "IsNew", "MimeType", "SeoFilename", "TitleAttribute", "VirtualPath" },
                values: new object[,]
                {
                    { 1050, "ut labore et dolore magna aliqu", null, true, "image/jpeg", "laboris nisi ut aliquip ex ea commodo conseq", "occaecat cupidatat non proident, sunt ", "/images/001050.jpeg" },
                    { 1051, "aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi", null, true, "image/jpeg", "officia deserunt mollit anim id est laborum.Lorem ip", "nulla pariatur. Excepteur si", "/images/001051.jpeg" },
                    { 1052, "quis nostrud exercitati", null, true, "image/jpeg", "ad", "mollit anim id est laborum.L", "/images/001052.jpeg" },
                    { 1056, "quis nostrud exercitation ullamco laboris nisi", null, true, "image/jpeg", "sit amet, consectetur ", "ut labore et dolore magna ", "/images/001056.jpeg" },
                    { 1054, "do eiusmod tempor inci", null, true, "image/jpeg", "esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat", "ex ea commodo consequat. Duis aute irure dolor in reprehenderit in volupt", "/images/001054.jpeg" },
                    { 1055, "adipiscing elit, sed do eiusmod", null, true, "image/jpeg", "cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lor", "sunt in culpa qui of", "/images/001055.jpeg" },
                    { 1057, "irure dolor in r", null, true, "image/jpeg", "culpa qui officia deserunt mollit anim id", "adipiscing elit, sed do eiusmod tempor incidid", "/images/001057.jpeg" },
                    { 1053, "velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat", null, true, "image/jpeg", "voluptate velit esse cillum dolore", "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,", "/images/001053.jpeg" },
                    { 1049, "proident, sunt in culpa qui officia deserunt mollit anim id est la", null, true, "image/jpeg", "reprehenderit in voluptate velit esse cillum", "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehen", "/images/001049.jpeg" },
                    { 1045, "nu", null, false, "image/jpeg", "anim ", "ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderi", "/images/001045.jpeg" },
                    { 1047, "nostrud exercitation ullamco laboris nisi ut aliq", null, true, "image/jpeg", "deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adip", "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id es", "/images/001047.jpeg" },
                    { 1046, "mollit anim i", null, true, "image/jpeg", "dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et", "ullamco laboris nisi ut aliquip ex ea commodo ", "/images/001046.jpeg" },
                    { 1044, "minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea", null, true, "image/jpeg", "mollit anim id est laborum.Lorem ipsum dolor sit ame", "ex ea commodo consequat. Duis au", "/images/001044.jpeg" },
                    { 1043, "aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ", null, true, "image/jpeg", "veniam, quis nostrud exercitation ullamco labori", "consectet", "/images/001043.jpeg" },
                    { 1042, "reprehenderit in volup", null, true, "image/jpeg", "dolor s", "sint occaecat cupidatat non proident, sunt in culpa qui officia deser", "/images/001042.jpeg" },
                    { 1091, "voluptate velit esse cillum dolore eu fugi", null, true, "image/jpeg", "proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor", "aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat ", "/images/001091.jpeg" },
                    { 1090, "est laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit,", null, true, "image/jpeg", "quis nostrud exercitation ullamco laboris nisi ut ", "incididunt ut labore et do", "/images/001090.jpeg" },
                    { 1089, "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit ", null, true, "image/jpeg", "elit, sed do eiusmod tempor incididunt ut labore et d", "s", "/images/001089.jpeg" },
                    { 1058, "officia deserunt mollit anim id est laborum.Lorem ipsum", null, true, "image/jpeg", "anim id est laborum.Lorem ipsum dolor sit amet, con", "aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamc", "/images/001058.jpeg" },
                    { 1048, "orum.Lorem ipsum dolor sit amet, consec", null, true, "image/jpeg", "Excepteur sint occaecat cupidatat non pr", "dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat", "/images/001048.jpeg" },
                    { 1059, "anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisc", null, true, "image/jpeg", "aliquip ex ea commodo con", "adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ", "/images/001059.jpeg" },
                    { 1071, "minim veniam, quis nostrud exercitation ullamco laboris nisi ", null, true, "image/jpeg", "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat", "sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et do", "/images/001071.jpeg" },
                    { 1061, "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco", null, false, "image/jpeg", "est laborum.Lorem ipsum dol", "amet, consectetur adipiscing elit, sed", "/images/001061.jpeg" },
                    { 1088, "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate ", null, true, "image/jpeg", "veniam, quis nostrud exerci", "in reprehenderit in voluptate velit esse cill", "/images/001088.jpeg" },
                    { 1010, "adipiscing elit,", null, true, "image/jpeg", "tempor incididunt ut labore et dolore magna aliqua. Ut e", "occaecat cupidatat non proident, sunt in culpa qui officia deserunt m", "/images/001010.jpeg" },
                    { 1009, "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehende", null, true, "image/jpeg", "in voluptate velit esse cillum dolore eu fugiat nulla pari", "in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla par", "/images/001009.jpeg" },
                    { 1008, "adipisc", null, true, "image/jpeg", "nostrud exe", "veniam, quis nostrud exercita", "/images/001008.jpeg" },
                    { 1007, "non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit", null, true, "image/jpeg", "et dolore magna a", "mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adip", "/images/001007.jpeg" },
                    { 1006, "commodo consequat. Duis aute irure dolor in reprehenderit in volupta", null, true, "image/jpeg", "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis", "Excepteur sint occaecat cupidatat", "/images/001006.jpeg" },
                    { 1005, "culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolo", null, true, "image/jpeg", "repre", "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitati", "/images/001005.jpeg" },
                    { 1004, "eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui of", null, true, "image/jpeg", "sint occaecat cupidatat", "cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat", "/images/001004.jpeg" },
                    { 1003, "qu", null, true, "image/jpeg", "anim id est laborum.Lorem ipsum dolor sit am", "elit, sed do eiusmod tempor incididunt ut labore et dolore magna", "/images/001003.jpeg" },
                    { 1060, "ex ea commodo consequat. Duis aute ir", null, false, "image/jpeg", "dolor in reprehenderit in voluptate veli", "m.Lorem ipsum dolor sit amet, co", "/images/001060.jpeg" },
                    { 1002, "ullamc", null, true, "image/jpeg", "consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore", "ullamco labori", "/images/001002.jpeg" },
                    { 1070, "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor", null, true, "image/jpeg", "adipiscing eli", "occaec", "/images/001070.jpeg" },
                    { 1069, "laboris nisi ut aliquip ex ea commodo cons", null, true, "image/jpeg", "elit, sed", "qui", "/images/001069.jpeg" },
                    { 1068, "exercitat", null, true, "image/jpeg", "in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor si", "in voluptate velit esse cillum dolore eu fugiat nulla pariat", "/images/001068.jpeg" },
                    { 1067, "minim veniam, quis nostrud exercitatio", null, true, "image/jpeg", "et dolore magna aliqua. U", "dolore magna aliqua. Ut enim ad minim veniam, quis nost", "/images/001067.jpeg" },
                    { 1066, "adipis", null, true, "image/jpeg", "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in", "ulla", "/images/001066.jpeg" },
                    { 1065, "nisi ut aliquip ex", null, true, "image/jpeg", "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur si", "exercitation ullamco laboris nisi ut aliquip ex ", "/images/001065.jpeg" },
                    { 1064, ".Lorem ipsum dolor sit amet, consectetur adipiscing eli", null, true, "image/jpeg", "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", "tempor incididunt ut labore et dolore magna aliqua. Ut en", "/images/001064.jpeg" },
                    { 1063, "sunt in culpa qui officia deserunt mollit", null, true, "image/jpeg", "aliqua. Ut enim ad minim veniam, quis nostrud exercitation u", "ullamco laboris nisi ut aliquip ex ea commod", "/images/001063.jpeg" },
                    { 1062, "in c", null, false, "image/jpeg", "Excepteur sint occaecat cupidatat n", "ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit es", "/images/001062.jpeg" },
                    { 1001, "adipiscing elit, sed do ", null, false, "image/jpeg", "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mo", "tempor incididunt ut labore et do", "/images/001001.jpeg" },
                    { 1087, "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo conseq", null, true, "image/jpeg", "adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim a", "culpa qui officia deserunt mollit anim id est labo", "/images/001087.jpeg" },
                    { 1086, "commodo consequat. Duis aute irure dolor in reprehenderit i", null, true, "image/jpeg", "cupidatat non proident, sunt in culpa qui offic", "Duis aute irure dolor in r", "/images/001086.jpeg" },
                    { 1085, "dolor sit amet, consectetur adipiscing elit, sed ", null, true, "image/jpeg", "amet, cons", "adipiscing elit, sed do eiusmod tempor incididunt ut labore et d", "/images/001085.jpeg" },
                    { 1031, "o", null, true, "image/jpeg", "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure", "nostrud", "/images/001031.jpeg" },
                    { 1030, "aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu f", null, true, "image/jpeg", "reprehenderit in voluptate", "dolor in reprehe", "/images/001030.jpeg" },
                    { 1029, "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in c", null, true, "image/jpeg", "irure dolor in reprehenderit in voluptate velit esse cillum dolore eu f", "voluptate ", "/images/001029.jpeg" },
                    { 1028, "adipiscing elit, ", null, true, "image/jpeg", "eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt i", "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderi", "/images/001028.jpeg" },
                    { 1026, "adi", null, true, "image/jpeg", "irure dolor in reprehenderit i", "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et do", "/images/001026.jpeg" },
                    { 1025, "sit amet, consectetur adipiscing elit, sed do eiusmod tempor ", null, true, "image/jpeg", "Ut enim ad minim venia", "veniam, quis nostrud exercitation ulla", "/images/001025.jpeg" },
                    { 1024, "id est laborum.Lorem ipsum dolor sit amet, consectetur adip", null, true, "image/jpeg", "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in repre", "anim id est laborum.Lorem ipsum dolor sit amet, co", "/images/001024.jpeg" },
                    { 1023, "minim veniam, quis ", null, true, "image/jpeg", "adipiscing elit, sed do eiusmod tempor incididunt ut l", "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidata", "/images/001023.jpeg" },
                    { 1022, "nisi ut aliquip ex ea commodo consequat. Du", null, true, "image/jpeg", "nulla pariatur. E", "veniam, quis nostrud exercitation ullamco laboris nisi ut aliqui", "/images/001022.jpeg" },
                    { 1020, "nulla pariatur. Ex", null, true, "image/jpeg", "magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ulla", "ipsum dolor sit amet, ", "/images/001020.jpeg" },
                    { 1019, "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in volupt", null, true, "image/jpeg", "eiusmod tempor incididunt ut labore et dolore magna al", "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sin", "/images/001019.jpeg" },
                    { 1018, "consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu ", null, true, "image/jpeg", "ut aliquip ex ea commodo consequat. Duis aute irure ", "ullam", "/images/001018.jpeg" },
                    { 1017, "nulla p", null, true, "image/jpeg", "consectetur adipiscing elit, sed do eiusmod tempor i", "occaecat cupidatat non proident, sunt in culpa qui officia des", "/images/001017.jpeg" },
                    { 1016, "adipiscing elit, sed do eiusmod tempo", null, false, "image/jpeg", "ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum d", "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo ", "/images/001016.jpeg" },
                    { 1015, "culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit ame", null, true, "image/jpeg", "dolor in reprehenderit in voluptate velit esse cillum dolore eu ", "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco l", "/images/001015.jpeg" },
                    { 1014, "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure ", null, true, "image/jpeg", "mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipiscing", "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidata", "/images/001014.jpeg" },
                    { 1013, "Ut enim ad minim ", null, true, "image/jpeg", "officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consect", "laboris nisi ut aliquip ex ea commodo consequat. ", "/images/001013.jpeg" },
                    { 1012, "consequat. Duis aute irure dolor in reprehenderit in ", null, true, "image/jpeg", "Duis aute irure dolor in reprehenderit in voluptate velit esse c", "labore et dolore magna aliqua. Ut enim ad minim ", "/images/001012.jpeg" },
                    { 1011, "elit, sed ", null, true, "image/jpeg", "deserunt mollit anim id est laboru", "nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culp", "/images/001011.jpeg" },
                    { 1032, "et dolore magna aliqua. Ut enim", null, true, "image/jpeg", "qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consecte", "aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat", "/images/001032.jpeg" },
                    { 1033, "anim id est laborum.L", null, true, "image/jpeg", "qui officia deserunt mollit anim id est laborum.Lor", "tempor incididunt ut labore et dolore magna ali", "/images/001033.jpeg" },
                    { 1021, "elit, sed do eiusmod tempor inci", null, true, "image/jpeg", "proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor", "amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor", "/images/001021.jpeg" },
                    { 1035, "velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non ", null, true, "image/jpeg", "nulla pariatur. Excepteur sint occaecat cupidatat ", "ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequa", "/images/001035.jpeg" },
                    { 1034, "mollit anim id est ", null, true, "image/jpeg", "Ut enim ad minim veniam, quis nostrud exerci", "ea commodo consequat. Duis aute irure dolor", "/images/001034.jpeg" },
                    { 1084, "orum.Lorem ipsum dolor sit amet, consectetur adipiscing elit", null, true, "image/jpeg", "dolor in reprehenderit in voluptate velit ", "veniam, quis nostrud exercitation ullamco laboris nisi", "/images/001084.jpeg" },
                    { 1083, "irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Ex", null, true, "image/jpeg", "proident, sunt in culpa qui officia d", "in culpa qui officia deserunt mollit anim id e", "/images/001083.jpeg" },
                    { 1082, "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehende", null, true, "image/jpeg", "eu fugiat nulla pariatur. Excepte", "do eiusmod tempor incididunt ut labore et dolore magna a", "/images/001082.jpeg" },
                    { 1081, "ni", null, true, "image/jpeg", "in voluptate velit esse cillum do", "pariatur. Excepteur sint occa", "/images/001081.jpeg" },
                    { 1080, "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad mi", null, true, "image/jpeg", "mollit anim id est laborum.Lorem ipsum d", "laboris nisi ut aliquip ex ", "/images/001080.jpeg" },
                    { 1078, "cillum dolore eu fugiat nulla pariatur. Except", null, true, "image/jpeg", "a", "enim ad m", "/images/001078.jpeg" },
                    { 1077, "consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat n", null, true, "image/jpeg", "um.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do", "qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipiscin", "/images/001077.jpeg" },
                    { 1076, "consequat. Duis aute irure dolor in reprehenderit in voluptate ve", null, true, "image/jpeg", "ex ea commodo co", "Ut enim ad minim ven", "/images/001076.jpeg" },
                    { 1075, "cillum dolore e", null, true, "image/jpeg", "ad minim veniam, qu", "velit esse cillum dolore eu fugiat nulla par", "/images/001075.jpeg" },
                    { 1079, "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure ", null, true, "image/jpeg", "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dol", "cupidatat non pr", "/images/001079.jpeg" },
                    { 1074, "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim", null, true, "image/jpeg", "culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, conse", "ipsum dol", "/images/001074.jpeg" },
                    { 1037, "pariatur. Excepteur sint occaecat cupidatat non proident, sunt", null, true, "image/jpeg", "dolore magna aliqua. Ut enim ad minim venia", "fugiat n", "/images/001037.jpeg" },
                    { 1038, "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nis", null, true, "image/jpeg", "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut ", "aute ir", "/images/001038.jpeg" },
                    { 1039, "sunt in culpa qui officia deserunt m", null, true, "image/jpeg", "elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam", "pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deseru", "/images/001039.jpeg" },
                    { 1040, "Duis aute irure d", null, true, "image/jpeg", "dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ull", "elit, sed do eiusmod t", "/images/001040.jpeg" },
                    { 1036, "id est laborum.Lorem ipsum dolor sit amet, c", null, true, "image/jpeg", "eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in cu", "Excepteur sint occaecat cupidatat no", "/images/001036.jpeg" },
                    { 1072, "sint occaecat cupidatat non proident, sunt in culpa qu", null, true, "image/jpeg", "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit", "cillum dol", "/images/001072.jpeg" },
                    { 1073, "nulla pariat", null, true, "image/jpeg", "anim id est laborum.Lorem", "tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nos", "/images/001073.jpeg" },
                    { 1041, "incididunt ut labore et dolore", null, true, "image/jpeg", "eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui offic", "cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est labor", "/images/001041.jpeg" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 64, "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in" },
                    { 72, "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptat" },
                    { 71, "in voluptate velit esse cillum do" },
                    { 70, "et dolore magna aliqua. U" },
                    { 69, "aliq" },
                    { 68, "velit esse " },
                    { 67, "elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut " },
                    { 66, "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud ex" },
                    { 65, "sunt in culpa qui officia deserunt mollit anim id" },
                    { 63, "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ull" },
                    { 52, "aute irure dolor in reprehenderit in" },
                    { 61, "quis nostrud exercitation ullamco " },
                    { 60, "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mo" },
                    { 59, "eu fugiat nulla pariatur. Excepteur sint occaec" },
                    { 58, "adipiscing elit, sed do eiusmod tempor " },
                    { 57, "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est " },
                    { 56, "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehen" },
                    { 55, "adipiscing elit, sed do eiusmod tempor in" },
                    { 54, "sunt in culpa qui officia d" },
                    { 53, "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehend" },
                    { 73, "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehen" },
                    { 62, "cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est la" },
                    { 74, "occaecat cupidatat non proident, sunt in culpa qui officia deser" },
                    { 96, "sit am" },
                    { 76, "dolor sit amet, co" },
                    { 98, "aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu " },
                    { 97, "sit amet, consectetur a" },
                    { 51, "sint occaecat cupidatat non proident, sunt " },
                    { 95, "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in r" },
                    { 94, "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Exce" },
                    { 93, "qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adi" },
                    { 92, "eu fugiat nulla pariatur. Excepteur sint occaec" },
                    { 91, "anim id est laborum.Lorem ipsum dolor sit amet, consectetur adi" },
                    { 90, "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mol" },
                    { 89, "proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum d" },
                    { 75, "qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor" },
                    { 88, "eiusmod tempor incididunt ut " },
                    { 86, "magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ulla" },
                    { 85, "aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliqu" },
                    { 84, "quis nostrud exercitation ullamco laboris nisi u" },
                    { 83, "proident, sunt in culpa qui officia deserunt mollit anim id est la" },
                    { 82, "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui o" },
                    { 81, "orum.Lo" },
                    { 80, "ex ea commodo consequat. Duis aute irure dolor in reprehend" },
                    { 79, "aute irure dolor in" },
                    { 78, "ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cill" },
                    { 77, "labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitatio" },
                    { 87, "ut labore et dolore magna " },
                    { 50, "consectetur adipiscing elit, sed" },
                    { 28, "sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore e" },
                    { 48, "laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor inci" },
                    { 21, "in voluptate velit esse cillum do" },
                    { 20, "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in rep" },
                    { 19, "magna aliqua. Ut" },
                    { 18, "tempor incididunt u" },
                    { 17, "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut al" },
                    { 16, "tempor incididunt ut labore " },
                    { 15, "ut labore et dolore magna aliqua. Ut enim ad minim v" },
                    { 14, "laborum.Lorem ipsum dolor sit amet, consectet" },
                    { 13, "laboris nisi ut aliquip ex ea commodo" },
                    { 12, "in voluptate velit esse cillum dolore eu fugiat nul" },
                    { 11, "est laborum.Lorem ipsum dolor sit amet, consectetur adi" },
                    { 10, "adipiscing elit, sed do eiusmod tempor incididunt ut l" },
                    { 9, "qui officia deserunt mollit anim id est laborum.Lorem ipsum dol" },
                    { 8, "ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat" },
                    { 7, "ullamco laboris nisi ut aliquip ex ea c" },
                    { 6, "consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore" },
                    { 5, "aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " },
                    { 4, "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat " },
                    { 3, "enim ad minim veniam, quis nostrud exercit" },
                    { 2, "nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in cul" },
                    { 1, "mollit anim id est l" },
                    { 22, "ex ea commodo consequat. Duis aute irure dolor in reprehende" },
                    { 23, "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis au" },
                    { 24, "m.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed" },
                    { 25, "veniam, quis nos" },
                    { 47, "eiusmod tempor incididunt ut labore et dolore magna " },
                    { 46, "ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehende" },
                    { 45, "ut lab" },
                    { 44, "enim ad minim veniam, quis nostrud exerci" },
                    { 43, "Excepteur sint occaecat cupidatat non proident, sunt in cul" },
                    { 42, "nostrud exercitation ullamco laboris nisi ut aliquip ex ea c" },
                    { 41, "occaecat cupidatat non proident, sunt in culpa" },
                    { 40, "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation " },
                    { 39, "in voluptate velit esse cillum dolor" },
                    { 38, "cupidatat non proid" },
                    { 49, "in culpa qui officia " },
                    { 37, "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia " },
                    { 35, "elit, sed d" },
                    { 34, "anim id est laborum.Lorem i" },
                    { 33, "nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in " },
                    { 32, "qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipiscing" },
                    { 31, "adipiscing elit, sed do eiusmod tempor incididunt ut labore " },
                    { 30, "ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate ve" },
                    { 29, "consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore" },
                    { 99, "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidata" },
                    { 27, "id est lab" },
                    { 26, "incididunt ut labore et dolore magn" },
                    { 36, "tempor incidi" },
                    { 100, "amet, consectetur adipiscing " }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentId", "PictureId" },
                values: new object[,]
                {
                    { 6, "sit ame", null, 1005 },
                    { 3, "consequ", null, 1020 },
                    { 4, "aliqu", null, 1066 },
                    { 5, "nulla par", null, 1037 },
                    { 2, "molli", null, 1024 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "Description", "Name", "Price", "Quantity", "ShortDescription" },
                values: new object[,]
                {
                    { 614, 71, "magna aliqua. Ut enim ad minim veniam, quis nostrud exercitat", "occaecat cupidatat non proid", 719484480.64m, 741, "mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipisc" },
                    { 766, 72, "amet, consectetur adipiscing", "sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ipsum dolor", 167867888.62m, 8194, "magna aliqua. Ut enim ad mini" },
                    { 296, 73, "et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ull", "dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididun", 1310967808.50m, 8169, "sunt in cu" },
                    { 767, 74, "anim id est laborum.Lorem ipsum dolor s", "nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui", 220467328.89m, 26266, "tempor incididunt ut labore et dolore magna aliqua. Ut enim " },
                    { 749, 76, "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deseru", "amet, consectetur adipiscing elit, sed ", 30524014.96m, 30593, "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolo" },
                    { 322, 78, "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore e", "dolor sit am", 1057469632.93m, 17058, "laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor inci" },
                    { 659, 82, ".", "sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore ma", 878492096.20m, 33, "non proident, sunt in culpa qui officia deserunt mollit anim id est" },
                    { 359, 70, "dolor in reprehenderit in voluptate velit esse cillum dolore eu", "labore et dolore magna aliqua. Ut enim ad minim veniam,", 75513120.55m, 30860, "Excepteur sint" },
                    { 762, 87, "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupida", "eu fugiat nulla pariatur. E", 1134310016.19m, 26105, "ut labore " },
                    { 763, 89, "sint occaecat cupi", "in voluptate velit esse cillum dolore eu fugiat nulla paria", 619129280.66m, 4870, "nulla pariatu" },
                    { 209, 90, "amet, consectetur adipiscing elit", "do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad m", 197639696.46m, 18354, "mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod t" },
                    { 624, 92, "aute irure dolor in reprehenderit in voluptate velit esse cillum dolor", "sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dol", 523793696.99m, 2273, "Excepteur sint occaecat cupidatat" },
                    { 206, 96, "do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostru", "adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim a", 1806602496.38m, 15296, "nostrud exe" },
                    { 668, 84, "eu fugiat nulla pariatur. Excepteur sint occaecat ", "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehe", 347287008.97m, 8897, "do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad " },
                    { 638, 86, "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur si", "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore", 115699600.54m, 10603, "do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostr" },
                    { 764, 67, "sed do eiusmod tempor incididunt ut labore et dolore", "laboris nisi ut aliquip ex ea commodo consequat. Duis aute i", 1073637760.90m, 1804, "mollit anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiu" },
                    { 782, 65, "id est laborum.Lorem ipsum dolor sit", "amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore", 7009875.65m, 26498, "velit esse cillum dolore eu fugiat nulla pariatur. " },
                    { 262, 8, "sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut ", "voluptate velit esse", 459097568.43m, 18985, "quis nostrud exercitation ulla" },
                    { 337, 9, "commodo consequat. Duis aute irure", "dolore eu fugiat nulla", 895406848.25m, 31382, "exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute ir" },
                    { 303, 12, "nisi ut aliquip ex ea commodo consequat. Dui", "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exerci", 937556928.24m, 25618, "aliq" },
                    { 427, 14, "ad minim veniam, quis nostrud exercitati", "Duis aute irure dolor in reprehenderit in voluptate velit ", 1725532160.78m, 10258, "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " },
                    { 724, 16, "sint oc", "aute irure dolor", 1686708992.57m, 24854, "est laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed" },
                    { 332, 19, "aliqua. Ut enim ad minim veniam, quis nostrud exercitation u", "irure dolor in reprehenderit in voluptate velit esse cill", 384726336.48m, 27551, "incididunt ut labore et dolore magna aliqua. Ut enim ad minim v" },
                    { 703, 20, "commodo consequat. Duis aute irure dolor in reprehenderit in volupta", "et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ulla", 1843203072.10m, 18830, "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate" },
                    { 784, 23, "in r", "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in", 230419680.50m, 1984, "adipiscing elit, sed do eiusmod tempor incididunt ut" },
                    { 772, 24, "in voluptate velit e", "minim veniam, quis n", 598342016.73m, 23806, "eu fugiat nu" },
                    { 627, 25, "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat no", "cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Lorem ip", 1152418688.50m, 17571, "mollit anim id est la" },
                    { 387, 32, "aliqua. Ut enim ad minim veniam, quis nostrud exercita", "ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in", 476802720.41m, 20934, "Duis aute irur" },
                    { 731, 37, "sit amet, consectetur adipiscing e", "sint occaecat cupidatat non proident, sunt in culpa qui officia d", 135786144.48m, 1320, "ipsum dolor sit amet, consectetur adipiscing eli" },
                    { 216, 43, "et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut ", "ullamco laboris ni", 1096621696.21m, 21042, "Duis aute irure do" },
                    { 219, 44, "nulla pariatur. Excepteur sint occaecat cupidatat no", "sint occaecat ", 393824000.32m, 30874, "esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cup" },
                    { 384, 50, "exercitation ullamco laboris nisi ut aliquip", "nulla pariatur. Exce", 1036171392.97m, 26147, "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit e" },
                    { 765, 54, "id est laborum.Lorem ipsum dol", "nulla pariatur. Excepteur sin", 548577344.50m, 12666, "pariatur. Excepteu" },
                    { 662, 59, "commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit ", "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo c", 1644037248.86m, 1303, "id est laborum.Lor" },
                    { 212, 66, "do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nos", "et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitat", 1199328128.13m, 16894, "anim id est laborum.Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor" },
                    { 295, 7, "exercitation ullamco laboris nisi ut aliquip ex ea commodo ", "sed do eiusmod tempor incididunt ut labore et dolore magna ali", 973344896.66m, 32209, "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentId", "PictureId" },
                values: new object[,]
                {
                    { 52, "labore ", 3, 1053 },
                    { 16, "sunt in", 6, 1012 },
                    { 12, "adipis", 3, 1059 },
                    { 27, "culpa qui o", 4, 1057 },
                    { 21, "aute iru", 4, 1030 },
                    { 8, "ex ea commod", 4, 1084 },
                    { 15, "ut labore", 3, 1036 },
                    { 28, "amet, consec", 5, 1065 },
                    { 19, "aliqua. Ut", 5, 1022 },
                    { 9, "aute ir", 5, 1017 },
                    { 14, "anim id", 2, 1081 },
                    { 11, "in voluptat", 2, 1089 },
                    { 18, "incidi", 3, 1051 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "ProductId", "CategoryId", "Id" },
                values: new object[,]
                {
                    { 766, 6, 2039 },
                    { 387, 3, 2041 },
                    { 322, 5, 2017 },
                    { 659, 4, 2042 },
                    { 772, 3, 2034 },
                    { 427, 6, 2028 }
                });

            migrationBuilder.InsertData(
                table: "ProductPictures",
                columns: new[] { "PictureId", "ProductId", "Id" },
                values: new object[,]
                {
                    { 1089, 764, 1039 },
                    { 1014, 767, 1061 },
                    { 1021, 296, 1090 },
                    { 1016, 296, 1075 },
                    { 1088, 766, 1063 },
                    { 1047, 766, 1034 },
                    { 1038, 766, 1056 },
                    { 1032, 766, 1021 },
                    { 1030, 614, 1017 },
                    { 1088, 359, 1024 },
                    { 1090, 764, 1084 },
                    { 1068, 765, 1045 },
                    { 1015, 212, 1065 },
                    { 1078, 782, 1059 },
                    { 1037, 782, 999 },
                    { 1031, 782, 1051 },
                    { 1052, 662, 1008 },
                    { 1038, 662, 1072 },
                    { 1028, 662, 1082 },
                    { 1034, 767, 1035 },
                    { 1062, 765, 1096 },
                    { 1045, 765, 1015 },
                    { 1030, 765, 1085 },
                    { 1032, 212, 1060 },
                    { 1076, 767, 1033 },
                    { 1028, 659, 1000 },
                    { 1067, 749, 1013 },
                    { 1047, 206, 1029 },
                    { 1020, 206, 1080 },
                    { 1090, 624, 1040 },
                    { 1055, 624, 1002 },
                    { 1011, 209, 1005 },
                    { 1070, 763, 1020 },
                    { 1038, 763, 1095 },
                    { 1036, 763, 1019 },
                    { 1016, 763, 1038 },
                    { 1083, 638, 1037 },
                    { 1065, 638, 1007 },
                    { 1065, 749, 1092 },
                    { 1028, 638, 1001 },
                    { 1084, 668, 1012 },
                    { 1068, 659, 1042 },
                    { 1063, 659, 1064 },
                    { 1039, 659, 1078 },
                    { 1044, 384, 1006 },
                    { 1012, 659, 1028 },
                    { 1079, 322, 1031 },
                    { 1036, 322, 1077 },
                    { 1029, 322, 1083 },
                    { 1084, 749, 1050 },
                    { 1082, 749, 1009 },
                    { 1089, 668, 1030 },
                    { 1043, 384, 1010 },
                    { 1010, 764, 1036 },
                    { 1006, 295, 1047 },
                    { 1073, 731, 1053 },
                    { 1049, 724, 1093 },
                    { 1038, 427, 1023 },
                    { 1023, 703, 1016 },
                    { 1040, 703, 1057 },
                    { 1031, 724, 1067 },
                    { 1066, 387, 1052 },
                    { 1074, 627, 1011 },
                    { 1034, 627, 1026 },
                    { 1002, 427, 1091 },
                    { 1008, 627, 1098 },
                    { 1013, 427, 1081 },
                    { 1039, 784, 1074 },
                    { 1006, 627, 1076 },
                    { 1060, 784, 1022 },
                    { 1044, 772, 1041 },
                    { 1041, 772, 1004 },
                    { 1018, 772, 1046 },
                    { 1028, 427, 1003 },
                    { 1079, 731, 1014 },
                    { 1023, 303, 1073 },
                    { 1044, 724, 1027 },
                    { 1040, 427, 1058 },
                    { 1054, 262, 1049 },
                    { 1057, 262, 1071 },
                    { 1072, 262, 1079 },
                    { 1006, 219, 1066 },
                    { 1089, 216, 1089 },
                    { 1002, 332, 1062 },
                    { 1048, 216, 1069 },
                    { 1070, 332, 1025 },
                    { 1043, 216, 1043 },
                    { 1037, 216, 1087 },
                    { 1071, 332, 1088 },
                    { 1013, 216, 1044 },
                    { 1023, 337, 1097 },
                    { 1032, 337, 1048 },
                    { 1079, 337, 1070 },
                    { 1053, 332, 1094 }
                });

            migrationBuilder.InsertData(
                table: "ProductTags",
                columns: new[] { "TagId", "ProductId", "Id" },
                values: new object[,]
                {
                    { 30, 427, 1056 },
                    { 62, 762, 1018 },
                    { 60, 427, 1038 },
                    { 55, 638, 1087 },
                    { 39, 762, 1027 },
                    { 65, 724, 1028 },
                    { 69, 668, 1078 },
                    { 17, 638, 1070 },
                    { 52, 638, 1055 },
                    { 48, 638, 1053 },
                    { 47, 638, 1014 },
                    { 56, 724, 1006 },
                    { 93, 659, 1081 },
                    { 77, 668, 1002 },
                    { 14, 209, 1072 },
                    { 83, 763, 1035 },
                    { 24, 295, 1003 },
                    { 72, 295, 1023 },
                    { 23, 295, 1033 },
                    { 31, 262, 1013 },
                    { 13, 262, 1062 },
                    { 45, 262, 1077 },
                    { 76, 262, 1080 },
                    { 15, 337, 1068 },
                    { 85, 337, 1083 },
                    { 87, 303, 1009 },
                    { 23, 206, 1064 },
                    { 54, 206, 1048 },
                    { 2, 303, 1085 },
                    { 10, 303, 1089 },
                    { 20, 624, 1100 },
                    { 7, 624, 1046 },
                    { 15, 624, 1042 },
                    { 25, 624, 1007 },
                    { 66, 659, 1029 },
                    { 82, 209, 1051 },
                    { 68, 209, 1004 },
                    { 84, 763, 1049 },
                    { 75, 763, 1037 },
                    { 90, 763, 1024 },
                    { 19, 659, 1017 },
                    { 67, 749, 1026 },
                    { 75, 724, 1092 },
                    { 58, 764, 1093 },
                    { 50, 764, 1034 },
                    { 15, 627, 1095 },
                    { 94, 387, 1057 },
                    { 87, 782, 1073 },
                    { 9, 782, 1063 },
                    { 61, 782, 1015 },
                    { 44, 782, 1011 },
                    { 75, 387, 1099 },
                    { 19, 662, 1082 },
                    { 69, 662, 1045 },
                    { 26, 662, 1020 },
                    { 90, 662, 1019 },
                    { 85, 731, 1036 },
                    { 65, 731, 1088 },
                    { 24, 765, 1031 },
                    { 10, 765, 1016 },
                    { 95, 384, 1098 },
                    { 63, 384, 1096 },
                    { 22, 384, 1071 },
                    { 73, 384, 1059 },
                    { 33, 384, 1039 },
                    { 35, 384, 1022 },
                    { 77, 216, 1050 },
                    { 18, 219, 1084 },
                    { 8, 764, 1094 },
                    { 32, 724, 1074 },
                    { 12, 359, 1021 },
                    { 74, 614, 1005 },
                    { 9, 322, 1060 },
                    { 20, 322, 1012 },
                    { 85, 332, 1052 },
                    { 29, 332, 1054 },
                    { 91, 749, 1030 },
                    { 80, 219, 1058 },
                    { 71, 749, 1025 },
                    { 81, 703, 1041 },
                    { 69, 703, 1097 },
                    { 33, 767, 1090 },
                    { 44, 767, 1075 },
                    { 27, 767, 1069 },
                    { 40, 784, 1001 },
                    { 59, 296, 1091 },
                    { 32, 296, 1067 },
                    { 50, 296, 1044 },
                    { 5, 296, 1008 },
                    { 71, 784, 1032 },
                    { 23, 784, 1061 },
                    { 94, 766, 1086 },
                    { 84, 766, 1066 },
                    { 79, 766, 1065 },
                    { 62, 766, 1043 },
                    { 72, 766, 1010 },
                    { 89, 784, 1079 },
                    { 40, 359, 1047 },
                    { 49, 219, 1076 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentId", "PictureId" },
                values: new object[,]
                {
                    { 49, "veniam, qu", 12, 1026 },
                    { 45, "rum.Lorem i", 21, 1067 },
                    { 38, "consequat.", 21, 1035 },
                    { 51, "mollit ani", 8, 1033 },
                    { 41, "dolor in", 8, 1043 },
                    { 24, "et dolore", 8, 1075 },
                    { 31, "et do", 9, 1019 },
                    { 46, "Excepteur", 14, 1013 },
                    { 25, "nisi u", 19, 1039 },
                    { 36, "cillum d", 18, 1079 },
                    { 40, "dolore eu", 15, 1060 },
                    { 23, "ullamco l", 15, 1046 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "ProductId", "CategoryId", "Id" },
                values: new object[,]
                {
                    { 772, 11, 2032 },
                    { 209, 11, 2018 },
                    { 784, 9, 2051 },
                    { 627, 21, 2021 },
                    { 764, 28, 2008 },
                    { 206, 52, 2009 },
                    { 784, 8, 2020 },
                    { 387, 8, 2022 },
                    { 206, 8, 2025 },
                    { 219, 12, 2003 },
                    { 322, 21, 2002 },
                    { 766, 11, 2043 },
                    { 767, 16, 2048 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentId", "PictureId" },
                values: new object[,]
                {
                    { 29, "consequat. ", 23, 1009 },
                    { 43, "cupida", 36, 1044 },
                    { 26, "cupidatat n", 24, 1090 },
                    { 33, "rum.Lorem", 24, 1031 },
                    { 48, "sint occaec", 38, 1006 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "ProductId", "CategoryId", "Id" },
                values: new object[,]
                {
                    { 322, 49, 2030 },
                    { 749, 40, 2049 },
                    { 295, 31, 2015 },
                    { 627, 31, 2044 },
                    { 766, 25, 2004 },
                    { 322, 25, 2010 },
                    { 638, 25, 2023 },
                    { 216, 41, 2001 },
                    { 668, 41, 2012 },
                    { 206, 51, 2024 },
                    { 216, 45, 2047 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentId", "PictureId" },
                values: new object[] { 44, "veniam, quis", 29, 1063 });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "ProductId", "CategoryId", "Id" },
                values: new object[,]
                {
                    { 763, 29, 2011 },
                    { 659, 29, 2027 },
                    { 212, 29, 2038 },
                    { 295, 43, 2029 },
                    { 765, 33, 2045 },
                    { 668, 33, 2050 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_PictureId",
                table: "Brands",
                column: "PictureId",
                unique: true,
                filter: "[PictureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_PictureId",
                table: "Categories",
                column: "PictureId",
                unique: true,
                filter: "[PictureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Customers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Customers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictures_ProductId",
                table: "ProductPictures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductId",
                table: "ProductTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "RegisteredRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "ProductPictures");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "RegisteredRoles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Pictures");
        }
    }
}
