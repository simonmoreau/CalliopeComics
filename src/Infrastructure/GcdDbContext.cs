using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Infrastructure
{
    public partial class GcdDbContext : DbContext, IGcdDbContext
    {
        public GcdDbContext()
        {
        }

        public GcdDbContext(DbContextOptions<GcdDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DjangoContentType> DjangoContentTypes { get; set; }

        public virtual DbSet<GcdAward> GcdAwards { get; set; }

        public virtual DbSet<GcdBiblioEntry> GcdBiblioEntries { get; set; }

        public virtual DbSet<GcdBrand> GcdBrands { get; set; }

        public virtual DbSet<GcdBrandEmblemGroup> GcdBrandEmblemGroups { get; set; }

        public virtual DbSet<GcdBrandGroup> GcdBrandGroups { get; set; }

        public virtual DbSet<GcdBrandUse> GcdBrandUses { get; set; }

        public virtual DbSet<GcdCharacter> GcdCharacters { get; set; }

        public virtual DbSet<GcdCharacterNameDetail> GcdCharacterNameDetails { get; set; }

        public virtual DbSet<GcdCharacterRelation> GcdCharacterRelations { get; set; }

        public virtual DbSet<GcdCharacterRelationType> GcdCharacterRelationTypes { get; set; }

        public virtual DbSet<GcdCharacterRole> GcdCharacterRoles { get; set; }

        public virtual DbSet<GcdCreator> GcdCreators { get; set; }

        public virtual DbSet<GcdCreatorArtInfluence> GcdCreatorArtInfluences { get; set; }

        public virtual DbSet<GcdCreatorDegree> GcdCreatorDegrees { get; set; }

        public virtual DbSet<GcdCreatorMembership> GcdCreatorMemberships { get; set; }

        public virtual DbSet<GcdCreatorNameDetail> GcdCreatorNameDetails { get; set; }

        public virtual DbSet<GcdCreatorNonComicWork> GcdCreatorNonComicWorks { get; set; }

        public virtual DbSet<GcdCreatorRelation> GcdCreatorRelations { get; set; }

        public virtual DbSet<GcdCreatorRelationCreatorName> GcdCreatorRelationCreatorNames { get; set; }

        public virtual DbSet<GcdCreatorSchool> GcdCreatorSchools { get; set; }

        public virtual DbSet<GcdCreatorSignature> GcdCreatorSignatures { get; set; }

        public virtual DbSet<GcdCreditType> GcdCreditTypes { get; set; }

        public virtual DbSet<GcdDegree> GcdDegrees { get; set; }

        public virtual DbSet<GcdFeature> GcdFeatures { get; set; }

        public virtual DbSet<GcdFeatureLogo> GcdFeatureLogos { get; set; }

        public virtual DbSet<GcdFeatureLogo2Feature> GcdFeatureLogo2Features { get; set; }

        public virtual DbSet<GcdFeatureRelation> GcdFeatureRelations { get; set; }

        public virtual DbSet<GcdFeatureRelationType> GcdFeatureRelationTypes { get; set; }

        public virtual DbSet<GcdFeatureType> GcdFeatureTypes { get; set; }

        public virtual DbSet<GcdGroup> GcdGroups { get; set; }

        public virtual DbSet<GcdGroupCharacter> GcdGroupCharacters { get; set; }

        public virtual DbSet<GcdGroupMembership> GcdGroupMemberships { get; set; }

        public virtual DbSet<GcdGroupMembershipType> GcdGroupMembershipTypes { get; set; }

        public virtual DbSet<GcdGroupNameDetail> GcdGroupNameDetails { get; set; }

        public virtual DbSet<GcdGroupRelation> GcdGroupRelations { get; set; }

        public virtual DbSet<GcdGroupRelationType> GcdGroupRelationTypes { get; set; }

        public virtual DbSet<GcdIndiciaPrinter> GcdIndiciaPrinters { get; set; }

        public virtual DbSet<GcdIndiciaPublisher> GcdIndiciaPublishers { get; set; }

        public virtual DbSet<GcdIssue> GcdIssues { get; set; }

        public virtual DbSet<GcdIssueBrandEmblem> GcdIssueBrandEmblems { get; set; }

        public virtual DbSet<GcdIssueCredit> GcdIssueCredits { get; set; }

        public virtual DbSet<GcdIssueIndiciaPrinter> GcdIssueIndiciaPrinters { get; set; }

        public virtual DbSet<GcdMembershipType> GcdMembershipTypes { get; set; }

        public virtual DbSet<GcdMultiverse> GcdMultiverses { get; set; }

        public virtual DbSet<GcdNameType> GcdNameTypes { get; set; }

        public virtual DbSet<GcdNonComicWorkRole> GcdNonComicWorkRoles { get; set; }

        public virtual DbSet<GcdNonComicWorkType> GcdNonComicWorkTypes { get; set; }

        public virtual DbSet<GcdNonComicWorkYear> GcdNonComicWorkYears { get; set; }

        public virtual DbSet<GcdPrinter> GcdPrinters { get; set; }

        public virtual DbSet<GcdPublisher> GcdPublishers { get; set; }

        public virtual DbSet<GcdReceivedAward> GcdReceivedAwards { get; set; }

        public virtual DbSet<GcdRelationType> GcdRelationTypes { get; set; }

        public virtual DbSet<GcdReprint> GcdReprints { get; set; }

        public virtual DbSet<GcdSchool> GcdSchools { get; set; }

        public virtual DbSet<GcdSearch> GcdSearches { get; set; }

        public virtual DbSet<GcdSearchConfig> GcdSearchConfigs { get; set; }

        public virtual DbSet<GcdSearchDatum> GcdSearchData { get; set; }

        public virtual DbSet<GcdSearchDocsize> GcdSearchDocsizes { get; set; }

        public virtual DbSet<GcdSearchIdx> GcdSearchIdxes { get; set; }

        public virtual DbSet<GcdSeries> GcdSeries { get; set; }

        public virtual DbSet<GcdSeriesBond> GcdSeriesBonds { get; set; }

        public virtual DbSet<GcdSeriesBondType> GcdSeriesBondTypes { get; set; }

        public virtual DbSet<GcdSeriesPublicationType> GcdSeriesPublicationTypes { get; set; }

        public virtual DbSet<GcdStory> GcdStories { get; set; }

        public virtual DbSet<GcdStoryCharacter> GcdStoryCharacters { get; set; }

        public virtual DbSet<GcdStoryCharacterGroup> GcdStoryCharacterGroups { get; set; }

        public virtual DbSet<GcdStoryCredit> GcdStoryCredits { get; set; }

        public virtual DbSet<GcdStoryFeatureLogo> GcdStoryFeatureLogos { get; set; }

        public virtual DbSet<GcdStoryFeatureObject> GcdStoryFeatureObjects { get; set; }

        public virtual DbSet<GcdStoryType> GcdStoryTypes { get; set; }

        public virtual DbSet<GcdStoryUniverse> GcdStoryUniverses { get; set; }

        public virtual DbSet<GcdUniverse> GcdUniverses { get; set; }

        public virtual DbSet<StddataCountry> StddataCountries { get; set; }

        public virtual DbSet<StddataDate> StddataDates { get; set; }

        public virtual DbSet<StddataLanguage> StddataLanguages { get; set; }

        public virtual DbSet<StddataScript> StddataScripts { get; set; }

        public virtual DbSet<TaggitTag> TaggitTags { get; set; }

        public virtual DbSet<TaggitTaggeditem> TaggitTaggeditems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            base.OnConfiguring(dbContextOptionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GcdBiblioEntry>(entity =>
            {
                entity.Property(e => e.StoryPtrId).ValueGeneratedNever();
                entity.Property(e => e.PageBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.PageEnded).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.StoryPtr).WithOne(p => p.GcdBiblioEntry).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdBrand>(entity =>
            {
                entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
                entity.Property(e => e.IssueCount).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearBeganUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearEnded).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearEndedUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearOverallBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearOverallEnded).HasDefaultValueSql("NULL");
            });

            modelBuilder.Entity<GcdBrandEmblemGroup>(entity =>
            {
                entity.HasOne(d => d.Brand).WithMany(p => p.GcdBrandEmblemGroups).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Brandgroup).WithMany(p => p.GcdBrandEmblemGroups).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdBrandGroup>(entity =>
            {
                entity.Property(e => e.YearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearEnded).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearOverallBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearOverallEnded).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Parent).WithMany(p => p.GcdBrandGroups).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdBrandUse>(entity =>
            {
                entity.Property(e => e.YearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearEnded).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Emblem).WithMany(p => p.GcdBrandUses).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Publisher).WithMany(p => p.GcdBrandUses).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCharacter>(entity =>
            {
                entity.Property(e => e.UniverseId).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearFirstPublished).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Language).WithMany(p => p.GcdCharacters).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Universe).WithMany(p => p.GcdCharacters).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCharacterNameDetail>(entity =>
            {
                entity.HasOne(d => d.Character).WithMany(p => p.GcdCharacterNameDetails).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCharacterRelation>(entity =>
            {
                entity.HasOne(d => d.FromCharacter).WithMany(p => p.GcdCharacterRelationFromCharacters).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RelationType).WithMany(p => p.GcdCharacterRelations).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ToCharacter).WithMany(p => p.GcdCharacterRelationToCharacters).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreator>(entity =>
            {
                entity.Property(e => e.BirthCountryId).HasDefaultValueSql("NULL");
                entity.Property(e => e.BirthDateId).HasDefaultValueSql("NULL");
                entity.Property(e => e.WhosWho).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.BirthCountry).WithMany(p => p.GcdCreatorBirthCountries).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.BirthDate).WithMany(p => p.GcdCreatorBirthDates).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DeathCountry).WithMany(p => p.GcdCreatorDeathCountries).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DeathDate).WithMany(p => p.GcdCreatorDeathDates).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreatorArtInfluence>(entity =>
            {
                entity.HasOne(d => d.Creator).WithMany(p => p.GcdCreatorArtInfluenceCreators).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.InfluenceLink).WithMany(p => p.GcdCreatorArtInfluenceInfluenceLinks).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreatorDegree>(entity =>
            {
                entity.Property(e => e.DegreeYear).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Creator).WithMany(p => p.GcdCreatorDegrees).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Degree).WithMany(p => p.GcdCreatorDegrees).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.School).WithMany(p => p.GcdCreatorDegrees).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreatorMembership>(entity =>
            {
                entity.Property(e => e.MembershipYearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.MembershipYearEnded).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Creator).WithMany(p => p.GcdCreatorMemberships).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.MembershipType).WithMany(p => p.GcdCreatorMemberships).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreatorNameDetail>(entity =>
            {
                entity.HasOne(d => d.Creator).WithMany(p => p.GcdCreatorNameDetails).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.InScript).WithMany(p => p.GcdCreatorNameDetails).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Type).WithMany(p => p.GcdCreatorNameDetails).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreatorNonComicWork>(entity =>
            {
                entity.HasOne(d => d.Creator).WithMany(p => p.GcdCreatorNonComicWorks).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.WorkRole).WithMany(p => p.GcdCreatorNonComicWorks).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.WorkType).WithMany(p => p.GcdCreatorNonComicWorks).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreatorRelation>(entity =>
            {
                entity.HasOne(d => d.FromCreator).WithMany(p => p.GcdCreatorRelationFromCreators).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RelationType).WithMany(p => p.GcdCreatorRelations).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ToCreator).WithMany(p => p.GcdCreatorRelationToCreators).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreatorRelationCreatorName>(entity =>
            {
                entity.HasOne(d => d.Creatornamedetail).WithMany(p => p.GcdCreatorRelationCreatorNames).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Creatorrelation).WithMany(p => p.GcdCreatorRelationCreatorNames).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreatorSchool>(entity =>
            {
                entity.Property(e => e.SchoolYearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.SchoolYearEnded).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Creator).WithMany(p => p.GcdCreatorSchools).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.School).WithMany(p => p.GcdCreatorSchools).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdCreatorSignature>(entity =>
            {
                entity.HasOne(d => d.Creator).WithMany(p => p.GcdCreatorSignatures).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdFeature>(entity =>
            {
                entity.Property(e => e.YearFirstPublished).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.FeatureType).WithMany(p => p.GcdFeatures).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Language).WithMany(p => p.GcdFeatures).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdFeatureLogo>(entity =>
            {
                entity.Property(e => e.YearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearEnded).HasDefaultValueSql("NULL");
            });

            modelBuilder.Entity<GcdFeatureLogo2Feature>(entity =>
            {
                entity.HasOne(d => d.Feature).WithMany(p => p.GcdFeatureLogo2Features).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Featurelogo).WithMany(p => p.GcdFeatureLogo2Features).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdFeatureRelation>(entity =>
            {
                entity.HasOne(d => d.FromFeature).WithMany(p => p.GcdFeatureRelationFromFeatures).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RelationType).WithMany(p => p.GcdFeatureRelations).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ToFeature).WithMany(p => p.GcdFeatureRelationToFeatures).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdGroup>(entity =>
            {
                entity.Property(e => e.UniverseId).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearFirstPublished).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Language).WithMany(p => p.GcdGroups).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Universe).WithMany(p => p.GcdGroups).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdGroupCharacter>(entity =>
            {
                entity.Property(e => e.UniverseId).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.GroupName).WithMany(p => p.GcdGroupCharacters).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Story).WithMany(p => p.GcdGroupCharacters).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Universe).WithMany(p => p.GcdGroupCharacters).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdGroupMembership>(entity =>
            {
                entity.Property(e => e.YearJoined).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearLeft).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Character).WithMany(p => p.GcdGroupMemberships).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Group).WithMany(p => p.GcdGroupMemberships).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.MembershipType).WithMany(p => p.GcdGroupMemberships).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdGroupNameDetail>(entity =>
            {
                entity.HasOne(d => d.Group).WithMany(p => p.GcdGroupNameDetails).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdGroupRelation>(entity =>
            {
                entity.HasOne(d => d.FromGroup).WithMany(p => p.GcdGroupRelationFromGroups).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RelationType).WithMany(p => p.GcdGroupRelations).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ToGroup).WithMany(p => p.GcdGroupRelationToGroups).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdIndiciaPrinter>(entity =>
            {
                entity.Property(e => e.YearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearEnded).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearOverallBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearOverallEnded).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Country).WithMany(p => p.GcdIndiciaPrinters).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Parent).WithMany(p => p.GcdIndiciaPrinters).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdIndiciaPublisher>(entity =>
            {
                entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
                entity.Property(e => e.IsSurrogate).HasDefaultValueSql("'0'");
                entity.Property(e => e.IssueCount).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearBeganUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearEnded).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearEndedUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearOverallBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearOverallEnded).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Country).WithMany(p => p.GcdIndiciaPublishers).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Parent).WithMany(p => p.GcdIndiciaPublishers).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdIssue>(entity =>
            {
                entity.Property(e => e.Barcode).HasDefaultValue("");
                entity.Property(e => e.BrandId).HasDefaultValueSql("NULL");
                entity.Property(e => e.Created).HasDefaultValue(new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
                entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
                entity.Property(e => e.DisplayVolumeWithNumber).HasDefaultValueSql("'0'");
                entity.Property(e => e.IndiciaFrequency).HasDefaultValue("");
                entity.Property(e => e.IndiciaPublisherId).HasDefaultValueSql("NULL");
                entity.Property(e => e.IsIndexed).HasDefaultValueSql("'0'");
                entity.Property(e => e.Isbn).HasDefaultValue("");
                entity.Property(e => e.Modified).HasDefaultValue(new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
                entity.Property(e => e.NoBarcode).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoEditing).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoIndiciaFrequency).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoIsbn).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoTitle).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoVolume).HasDefaultValueSql("'0'");
                entity.Property(e => e.OnSaleDateUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.PageCount).HasDefaultValueSql("NULL");
                entity.Property(e => e.PageCountUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.Title).HasDefaultValue("");
                entity.Property(e => e.ValidIsbn).HasDefaultValue("");
                entity.Property(e => e.VariantName).HasDefaultValue("");
                entity.Property(e => e.VariantOfId).HasDefaultValueSql("NULL");
                entity.Property(e => e.Volume).HasDefaultValue("");

                entity.HasOne(d => d.Brand).WithMany(p => p.GcdIssues).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.IndiciaPublisher).WithMany(p => p.GcdIssues).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Series).WithMany(p => p.GcdIssues).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.VariantOf).WithMany(p => p.InverseVariantOf).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdIssueBrandEmblem>(entity =>
            {
                entity.HasOne(d => d.Brand).WithMany(p => p.GcdIssueBrandEmblems).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Issue).WithMany(p => p.GcdIssueBrandEmblems).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<GcdIssueCredit>(entity =>
            {
                entity.HasOne(d => d.Creator).WithMany(p => p.GcdIssueCredits).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.CreditType).WithMany(p => p.GcdIssueCredits).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Issue).WithMany(p => p.GcdIssueCredits).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdIssueIndiciaPrinter>(entity =>
            {
                entity.HasOne(d => d.Indiciaprinter).WithMany(p => p.GcdIssueIndiciaPrinters).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Issue).WithMany(p => p.GcdIssueIndiciaPrinters).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdMultiverse>(entity =>
            {
                entity.HasOne(d => d.Mainstream).WithMany(p => p.GcdMultiverses).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdNonComicWorkYear>(entity =>
            {
                entity.Property(e => e.WorkYear).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.NonComicWork).WithMany(p => p.GcdNonComicWorkYears).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdPrinter>(entity =>
            {
                entity.Property(e => e.YearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearEnded).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearOverallBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearOverallEnded).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Country).WithMany(p => p.GcdPrinters).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdPublisher>(entity =>
            {
                entity.Property(e => e.BrandCount).HasDefaultValueSql("'0'");
                entity.Property(e => e.Created).HasDefaultValue(new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
                entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
                entity.Property(e => e.IndiciaPublisherCount).HasDefaultValueSql("'0'");
                entity.Property(e => e.IssueCount).HasDefaultValueSql("'0'");
                entity.Property(e => e.Modified).HasDefaultValue(new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
                entity.Property(e => e.SeriesCount).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearBeganUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearEnded).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearEndedUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearOverallBegan).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearOverallEnded).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Country).WithMany(p => p.GcdPublishers).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdReceivedAward>(entity =>
            {
                entity.Property(e => e.AwardId).HasDefaultValueSql("NULL");
                entity.Property(e => e.AwardYear).HasDefaultValueSql("NULL");
                entity.Property(e => e.ContentTypeId).HasDefaultValueSql("NULL");
                entity.Property(e => e.ObjectId).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Award).WithMany(p => p.GcdReceivedAwards).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ContentType).WithMany(p => p.GcdReceivedAwards).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdReprint>(entity =>
            {
                entity.Property(e => e.OriginId).HasDefaultValueSql("NULL");
                entity.Property(e => e.TargetId).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Origin).WithMany(p => p.GcdReprintOrigins).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.OriginIssue).WithMany(p => p.GcdReprintOriginIssues).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Target).WithMany(p => p.GcdReprintTargets).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TargetIssue).WithMany(p => p.GcdReprintTargetIssues).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdSearchDatum>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<GcdSearchDocsize>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<GcdSeries>(entity =>
            {
                entity.Property(e => e.Created).HasDefaultValue(new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
                entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
                entity.Property(e => e.FirstIssueId).HasDefaultValueSql("NULL");
                entity.Property(e => e.Format).HasDefaultValue("");
                entity.Property(e => e.HasBarcode).HasDefaultValueSql("'1'");
                entity.Property(e => e.HasGallery).HasDefaultValueSql("'0'");
                entity.Property(e => e.HasIndiciaFrequency).HasDefaultValueSql("'1'");
                entity.Property(e => e.HasIsbn).HasDefaultValueSql("'1'");
                entity.Property(e => e.HasIssueTitle).HasDefaultValueSql("'0'");
                entity.Property(e => e.HasVolume).HasDefaultValueSql("'1'");
                entity.Property(e => e.IsComicsPublication).HasDefaultValueSql("'1'");
                entity.Property(e => e.IsCurrent).HasDefaultValueSql("'0'");
                entity.Property(e => e.LastIssueId).HasDefaultValueSql("NULL");
                entity.Property(e => e.Modified).HasDefaultValue(new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
                entity.Property(e => e.PublicationDates).HasDefaultValue("");
                entity.Property(e => e.YearBeganUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.YearEnded).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearEndedUncertain).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Country).WithMany(p => p.GcdSeries).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.FirstIssue).WithMany(p => p.GcdSeriesFirstIssues).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Language).WithMany(p => p.GcdSeries).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.LastIssue).WithMany(p => p.GcdSeriesLastIssues).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PublicationType).WithMany(p => p.GcdSeries).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Publisher).WithMany(p => p.GcdSeries).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdSeriesBond>(entity =>
            {
                entity.Property(e => e.OriginIssueId).HasDefaultValueSql("NULL");
                entity.Property(e => e.TargetIssueId).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.BondType).WithMany(p => p.GcdSeriesBonds).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Origin).WithMany(p => p.GcdSeriesBondOrigins).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.OriginIssue).WithMany(p => p.GcdSeriesBondOriginIssues).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Target).WithMany(p => p.GcdSeriesBondTargets).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TargetIssue).WithMany(p => p.GcdSeriesBondTargetIssues).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdStory>(entity =>
            {
                entity.Property(e => e.Created).HasDefaultValue(new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
                entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
                entity.Property(e => e.Genre).HasDefaultValue("");
                entity.Property(e => e.JobNumber).HasDefaultValue("");
                entity.Property(e => e.Modified).HasDefaultValue(new DateTime(1901, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
                entity.Property(e => e.NoColors).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoEditing).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoInks).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoLetters).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoPencils).HasDefaultValueSql("'0'");
                entity.Property(e => e.NoScript).HasDefaultValueSql("'0'");
                entity.Property(e => e.PageCount).HasDefaultValueSql("NULL");
                entity.Property(e => e.PageCountUncertain).HasDefaultValueSql("'0'");
                entity.Property(e => e.Title).HasDefaultValue("");
                entity.Property(e => e.TitleInferred).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Issue).WithMany(p => p.GcdStories).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Type).WithMany(p => p.GcdStories).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdStoryCharacter>(entity =>
            {
                entity.Property(e => e.GroupUniverseId).HasDefaultValueSql("NULL");
                entity.Property(e => e.RoleId).HasDefaultValueSql("NULL");
                entity.Property(e => e.UniverseId).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Character).WithMany(p => p.GcdStoryCharacters).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.GroupUniverse).WithMany(p => p.GcdStoryCharacterGroupUniverses).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Role).WithMany(p => p.GcdStoryCharacters).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Story).WithMany(p => p.GcdStoryCharacters).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Universe).WithMany(p => p.GcdStoryCharacterUniverses).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdStoryCharacterGroup>(entity =>
            {
                entity.HasOne(d => d.Group).WithMany(p => p.GcdStoryCharacterGroups).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Storycharacter).WithMany(p => p.GcdStoryCharacterGroups).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdStoryCredit>(entity =>
            {
                entity.Property(e => e.SignatureId).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Creator).WithMany(p => p.GcdStoryCredits).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.CreditType).WithMany(p => p.GcdStoryCredits).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Signature).WithMany(p => p.GcdStoryCredits).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Story).WithMany(p => p.GcdStoryCredits).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdStoryFeatureLogo>(entity =>
            {
                entity.HasOne(d => d.Featurelogo).WithMany(p => p.GcdStoryFeatureLogos).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Story).WithMany(p => p.GcdStoryFeatureLogos).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdStoryFeatureObject>(entity =>
            {
                entity.HasOne(d => d.Feature).WithMany(p => p.GcdStoryFeatureObjects).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Story).WithMany(p => p.GcdStoryFeatureObjects).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdStoryUniverse>(entity =>
            {
                entity.HasOne(d => d.Story).WithMany(p => p.GcdStoryUniverses).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Universe).WithMany(p => p.GcdStoryUniverses).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GcdUniverse>(entity =>
            {
                entity.Property(e => e.VerseId).HasDefaultValueSql("NULL");
                entity.Property(e => e.YearFirstPublished).HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Verse).WithMany(p => p.GcdUniverses).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TaggitTaggeditem>(entity =>
            {
                entity.HasOne(d => d.ContentType).WithMany(p => p.TaggitTaggeditems).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Tag).WithMany(p => p.TaggitTaggeditems).OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}