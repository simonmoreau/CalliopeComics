using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{

    public interface IGcdDbContext
    {
        DbSet<DjangoContentType> DjangoContentTypes { get; set; }
        DbSet<GcdAward> GcdAwards { get; set; }
        DbSet<GcdBiblioEntry> GcdBiblioEntries { get; set; }
        DbSet<GcdBrand> GcdBrands { get; set; }
        DbSet<GcdBrandEmblemGroup> GcdBrandEmblemGroups { get; set; }
        DbSet<GcdBrandGroup> GcdBrandGroups { get; set; }
        DbSet<GcdBrandUse> GcdBrandUses { get; set; }
        DbSet<GcdCharacter> GcdCharacters { get; set; }
        DbSet<GcdCharacterNameDetail> GcdCharacterNameDetails { get; set; }
        DbSet<GcdCharacterRelation> GcdCharacterRelations { get; set; }
        DbSet<GcdCharacterRelationType> GcdCharacterRelationTypes { get; set; }
        DbSet<GcdCharacterRole> GcdCharacterRoles { get; set; }
        DbSet<GcdCreator> GcdCreators { get; set; }
        DbSet<GcdCreatorArtInfluence> GcdCreatorArtInfluences { get; set; }
        DbSet<GcdCreatorDegree> GcdCreatorDegrees { get; set; }
        DbSet<GcdCreatorMembership> GcdCreatorMemberships { get; set; }
        DbSet<GcdCreatorNameDetail> GcdCreatorNameDetails { get; set; }
        DbSet<GcdCreatorNonComicWork> GcdCreatorNonComicWorks { get; set; }
        DbSet<GcdCreatorRelation> GcdCreatorRelations { get; set; }
        DbSet<GcdCreatorRelationCreatorName> GcdCreatorRelationCreatorNames { get; set; }
        DbSet<GcdCreatorSchool> GcdCreatorSchools { get; set; }
        DbSet<GcdCreatorSignature> GcdCreatorSignatures { get; set; }
        DbSet<GcdCreditType> GcdCreditTypes { get; set; }
        DbSet<GcdDegree> GcdDegrees { get; set; }
        DbSet<GcdFeature> GcdFeatures { get; set; }
        DbSet<GcdFeatureLogo> GcdFeatureLogos { get; set; }
        DbSet<GcdFeatureLogo2Feature> GcdFeatureLogo2Features { get; set; }
        DbSet<GcdFeatureRelation> GcdFeatureRelations { get; set; }
        DbSet<GcdFeatureRelationType> GcdFeatureRelationTypes { get; set; }
        DbSet<GcdFeatureType> GcdFeatureTypes { get; set; }
        DbSet<GcdGroup> GcdGroups { get; set; }
        DbSet<GcdGroupCharacter> GcdGroupCharacters { get; set; }
        DbSet<GcdGroupMembership> GcdGroupMemberships { get; set; }
        DbSet<GcdGroupMembershipType> GcdGroupMembershipTypes { get; set; }
        DbSet<GcdGroupNameDetail> GcdGroupNameDetails { get; set; }
        DbSet<GcdGroupRelation> GcdGroupRelations { get; set; }
        DbSet<GcdGroupRelationType> GcdGroupRelationTypes { get; set; }
        DbSet<GcdIndiciaPrinter> GcdIndiciaPrinters { get; set; }
        DbSet<GcdIndiciaPublisher> GcdIndiciaPublishers { get; set; }
        DbSet<GcdIssue> GcdIssues { get; set; }
        DbSet<GcdIssueBrandEmblem> GcdIssueBrandEmblems { get; set; }
        DbSet<GcdIssueCredit> GcdIssueCredits { get; set; }
        DbSet<GcdIssueIndiciaPrinter> GcdIssueIndiciaPrinters { get; set; }
        DbSet<GcdMembershipType> GcdMembershipTypes { get; set; }
        DbSet<GcdMultiverse> GcdMultiverses { get; set; }
        DbSet<GcdNameType> GcdNameTypes { get; set; }
        DbSet<GcdNonComicWorkRole> GcdNonComicWorkRoles { get; set; }
        DbSet<GcdNonComicWorkType> GcdNonComicWorkTypes { get; set; }
        DbSet<GcdNonComicWorkYear> GcdNonComicWorkYears { get; set; }
        DbSet<GcdPrinter> GcdPrinters { get; set; }
        DbSet<GcdPublisher> GcdPublishers { get; set; }
        DbSet<GcdReceivedAward> GcdReceivedAwards { get; set; }
        DbSet<GcdRelationType> GcdRelationTypes { get; set; }
        DbSet<GcdReprint> GcdReprints { get; set; }
        DbSet<GcdSchool> GcdSchools { get; set; }
        DbSet<GcdSearch> GcdSearches { get; set; }
        DbSet<GcdSearchConfig> GcdSearchConfigs { get; set; }
        DbSet<GcdSearchDatum> GcdSearchData { get; set; }
        DbSet<GcdSearchDocsize> GcdSearchDocsizes { get; set; }
        DbSet<GcdSearchIdx> GcdSearchIdxes { get; set; }
        DbSet<GcdSeries> GcdSeries { get; set; }
        DbSet<GcdSeriesBond> GcdSeriesBonds { get; set; }
        DbSet<GcdSeriesBondType> GcdSeriesBondTypes { get; set; }
        DbSet<GcdSeriesPublicationType> GcdSeriesPublicationTypes { get; set; }
        DbSet<GcdStory> GcdStories { get; set; }
        DbSet<GcdStoryCharacter> GcdStoryCharacters { get; set; }
        DbSet<GcdStoryCharacterGroup> GcdStoryCharacterGroups { get; set; }
        DbSet<GcdStoryCredit> GcdStoryCredits { get; set; }
        DbSet<GcdStoryFeatureLogo> GcdStoryFeatureLogos { get; set; }
        DbSet<GcdStoryFeatureObject> GcdStoryFeatureObjects { get; set; }
        DbSet<GcdStoryType> GcdStoryTypes { get; set; }
        DbSet<GcdStoryUniverse> GcdStoryUniverses { get; set; }
        DbSet<GcdUniverse> GcdUniverses { get; set; }
        DbSet<StddataCountry> StddataCountries { get; set; }
        DbSet<StddataDate> StddataDates { get; set; }
        DbSet<StddataLanguage> StddataLanguages { get; set; }
        DbSet<StddataScript> StddataScripts { get; set; }
        DbSet<TaggitTag> TaggitTags { get; set; }
        DbSet<TaggitTaggeditem> TaggitTaggeditems { get; set; }
    }
}