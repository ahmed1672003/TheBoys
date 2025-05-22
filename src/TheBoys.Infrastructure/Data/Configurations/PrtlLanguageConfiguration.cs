using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheBoys.Infrastructure.Data.Configurations;

internal sealed class PrtlLanguageConfiguration : IEntityTypeConfiguration<PrtlLanguage>
{
    public void Configure(EntityTypeBuilder<PrtlLanguage> builder)
    {
        builder.HasKey(e => e.LangId).HasName("Pk_Languages");
        builder.ToTable(
            "prtl_Languages",
            tb => tb.HasComment("يحتوى على اللغات المعتمدة فى كل تفاصيل قاعدة البيانات")
        );
        builder.Property(e => e.LangId).HasComment("معرف  اللغة المستخدمة");
        builder.Property(e => e.Lcid).HasComment("كود اللهجة المحلية");
    }
}
