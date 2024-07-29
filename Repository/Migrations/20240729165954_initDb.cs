using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO Currencies (Name, Country) VALUES 
            ('Рубль', 'Россия'),
            ('Доллар', 'США'),
            ('Евро', 'Евросоюз');
        ");

            migrationBuilder.Sql(@"
            INSERT INTO Deposits (Name, MinimumTerm, MinimumAmount, CurrencyId, InterestRate, AdditionalConditions) VALUES 
            ('Срочный вклад', 6, 10000, 1, 5.5, 'Без дополнительных условий'),
            ('Накопительный вклад', 12, 5000, 2, 3.5, 'Пополнение возможно'),
            ('Пенсионный вклад', 24, 1000, 3, 2.5, 'Для пенсионеров');
        ");

            migrationBuilder.Sql(@"
            INSERT INTO ExchangeRates (Date, CurrencyId, Rate) VALUES 
            ('2024-01-01', 1, 74.5),
            ('2024-01-01', 2, 1.0),
            ('2024-01-01', 3, 0.85);
        ");

            migrationBuilder.Sql(@"
            INSERT INTO Depositors (FullName, Address, Phone, PassportDetails) VALUES 
            ('Иванов Иван Иванович', 'г. Москва, ул. Ленина, д. 1', '89001234567', '1234 567890'),
            ('Петров Петр Петрович', 'г. Санкт-Петербург, пр. Невский, д. 2', '89007654321', '2345 678901'),
            ('Сидоров Сидор Сидорович', 'г. Новосибирск, ул. Красный проспект, д. 3', '89009876543', '3456 789012');
        ");

            migrationBuilder.Sql(@"
            INSERT INTO Operations (DepositorId, DepositDate, ReturnDate, DepositId, DepositAmount, ReturnAmount, IsReturned) VALUES 
            (1, '2024-01-10', NULL, 1, 15000, NULL, 0),
            (2, '2024-02-20', NULL, 2, 7000, NULL, 0),
            (3, '2024-03-15', NULL, 3, 2000, NULL, 0);
        ");

            migrationBuilder.Sql(@"
            INSERT INTO Employees (FullName, Position) VALUES 
            ('Смирнов Алексей Алексеевич', 'Менеджер'),
            ('Кузнецова Мария Ивановна', 'Консультант'),
            ('Васильев Дмитрий Сергеевич', 'Кассир');
        ");

            migrationBuilder.Sql(@"
            INSERT INTO EmployeeOperations (EmployeeId, OperationId) VALUES 
            (1, 1),
            (2, 2),
            (3, 3);
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM EmployeeOperations;");
            migrationBuilder.Sql("DELETE FROM Employees;");
            migrationBuilder.Sql("DELETE FROM Operations;");
            migrationBuilder.Sql("DELETE FROM Depositors;");
            migrationBuilder.Sql("DELETE FROM ExchangeRates;");
            migrationBuilder.Sql("DELETE FROM Deposits;");
            migrationBuilder.Sql("DELETE FROM Currencies;");
        }
    }
}
