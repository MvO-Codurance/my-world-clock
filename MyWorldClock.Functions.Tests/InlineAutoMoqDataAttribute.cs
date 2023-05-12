using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace MyWorldClock.Functions.Tests;

/// <summary>
/// Inline auto moq data attribute to allow for inline auto fixture test models as well as Moq based items
/// </summary>
public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] values)
        : base(new AutoMoqDataAttribute(), values)
    {
    }

    /// <summary>
    /// Private attribute which is required to be passed in. This sets up the <see cref="IFixture"/> to hook Moq into the pipeline.
    /// </summary>
    private class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Customize(new CompositeCustomization(new AutoMoqCustomization()));
                return fixture;
            })
        {
        }
    }
}