using FluentMigrator;

namespace BizBorrowDiary.Database.Migrations
{
    //yyyymmddhhmm
    [Migration(202401010101)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table(TableNames.Address)
            .WithColumn("Id").AsFixedLengthString(26).NotNullable().PrimaryKey($"pk_{TableNames.Address}_Id")
            .WithColumn("Addres1").AsString(250).NotNullable()//House no/village
            .WithColumn("Addres2").AsString(250).Nullable() //tower /floor/ village/society
            .WithColumn("City").AsString(100).Nullable() //PO/Tehseel/pergana
            .WithColumn("District").AsString(100).Nullable() //district
            .WithColumn("State").AsString(100).Nullable()
            .WithColumn("PipCode").AsString(20).Nullable()
            .WithColumn("Phone").AsString(20).Nullable()
            .WithColumn("IsPermanent").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("SyncId").AsString(36).Nullable()
            .WithColumn("IsEnabled").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("CreatedBy").AsString(255).NotNullable()
            .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefaultValue(DateTime.UtcNow)
            .WithColumn("UpdatedBy").AsString(255).Nullable()
            .WithColumn("UpdatedOn").AsDateTime().Nullable();

            Create.Table(TableNames.Owner)
              .WithColumn("Id").AsFixedLengthString(26).NotNullable().PrimaryKey($"pk_{TableNames.Owner}_Id")
              .WithColumn("OwnerName").AsString(200).NotNullable()
              .WithColumn("ContactEmail").AsString(255).Nullable()
              .WithColumn("ContactPhone").AsString(20).Nullable()
              .WithColumn("AddressId").AsString(26).Nullable().ForeignKey($"fk_{TableNames.Owner}_{TableNames.Address}", $"{TableNames.Address}", "Id")
              .WithColumn("SyncId").AsString(36).Nullable()
              .WithColumn("IsEnabled").AsBoolean().NotNullable().WithDefaultValue(true)
              .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
              .WithColumn("CreatedBy").AsString(255).NotNullable()
              .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefaultValue(DateTime.UtcNow)
              .WithColumn("UpdatedBy").AsString(255).Nullable()
              .WithColumn("UpdatedOn").AsDateTime().Nullable();

            Create.Table(TableNames.BizUnit)
               .WithColumn("Id").AsFixedLengthString(26).NotNullable().PrimaryKey($"pk_{TableNames.BizUnit}_Id")
               .WithColumn("ShopNickName").AsString(200).NotNullable()
               .WithColumn("ShopName").AsString(200).NotNullable()
               .WithColumn("OwnerId").AsString(26).Nullable().ForeignKey($"fk_{TableNames.BizUnit}_{TableNames.Owner}", $"{TableNames.Owner}", "Id")
               .WithColumn("ShopPhone").AsString(20).Nullable()
               .WithColumn("AddressId").AsString(26).Nullable().ForeignKey($"fk_{TableNames.BizUnit}_{TableNames.Address}", $"{TableNames.Address}", "Id")
               .WithColumn("SyncId").AsString(36).Nullable()
               .WithColumn("IsEnabled").AsBoolean().NotNullable().WithDefaultValue(true)
               .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
               .WithColumn("CreatedBy").AsString(255).NotNullable()
               .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefaultValue(DateTime.UtcNow)
               .WithColumn("UpdatedBy").AsString(255).Nullable()
               .WithColumn("UpdatedOn").AsDateTime().Nullable();

            Create.Table(TableNames.Customer)
                .WithColumn("Id").AsFixedLengthString(26).NotNullable().PrimaryKey($"pk_{TableNames.Customer}_Id")
                .WithColumn("CustomerName").AsString(200).NotNullable()
                .WithColumn("ContactEmail").AsString(255).Nullable()
                .WithColumn("ContactPhone").AsString(20).Nullable()
                .WithColumn("AddressId").AsString(26).Nullable().ForeignKey($"fk_{TableNames.Customer}_{TableNames.Address}", $"{TableNames.Address}", "Id")
                .WithColumn("BizUnitId").AsString(26).Nullable().ForeignKey($"fk_{TableNames.Customer}_{TableNames.BizUnit}", $"{TableNames.BizUnit}", "Id")
                .WithColumn("SyncId").AsString(36).Nullable()
                .WithColumn("IsEnabled").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("CreatedBy").AsString(255).NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefaultValue(DateTime.UtcNow)
                .WithColumn("UpdatedBy").AsString(255).Nullable()
                .WithColumn("UpdatedOn").AsDateTime().Nullable();

        }
        public override void Down()
        {
            Delete.Table($"{TableNames.BizUnit}");
            Delete.Table($"{TableNames.Customer}");
            Delete.Table($"{TableNames.Owner}");
            Delete.Table($"{TableNames.Address}");
        }
    }
}
