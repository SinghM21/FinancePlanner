using FinancePlanner.DTOs;
using FinancePlanner.Mappers;
using FinancePlanner.Models.Investment;
using FinancePlanner.ViewModels;
using JetBrains.Annotations;

namespace FinancePlannerTests.Mappers;

[TestSubject(typeof(InvestmentMapper))]
public class InvestmentMapperTest
{
    private readonly InvestmentMapper _investmentMapper = new InvestmentMapper();

    [Fact]
    public void MapToDTO_ViewModel_MapsAllFields()
    {
        //Arrange
        var investmentVm = new InvestmentViewModel()
        {
            ID = 1,
            Name = "Investment A",
            Description = "Description A",
            Type = InvestmentType.Stocks,
            Quantity = 10,
            Cost = 100,
            Recurring = true,
            Frequency = FrequencyType.Quarterly,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2024, 1, 1)
        };

        //Act
        var investmentDto = _investmentMapper.MapToDTO(investmentVm);

        //Assert
        Assert.Equal(investmentVm.ID, investmentDto.ID);
        Assert.Equal(investmentVm.Name, investmentDto.Name);
        Assert.Equal(investmentVm.Description, investmentDto.Description);
        Assert.Equal(investmentVm.Type, investmentDto.Type);
        Assert.Equal(investmentVm.Quantity, investmentDto.Quantity);
        Assert.Equal(investmentVm.Cost, investmentDto.Cost);
        Assert.Equal(investmentVm.Recurring, investmentDto.Recurring);
        Assert.Equal(investmentVm.Frequency, investmentDto.Frequency);
        Assert.Equal(investmentVm.StartDate, investmentDto.StartDate);
        Assert.Equal(investmentVm.EndDate, investmentDto.EndDate);
    }

    [Fact]
    public void MapToDTO_Investment_MapsAllFields()
    {
        //Arrange
        var investment = new Investment()
        {
            ID = 1,
            Name = "Investment A",
            Description = "Description A",
            Type = InvestmentType.Stocks,
            Quantity = 10,
            Cost = 100,
            Recurring = true,
            Frequency = FrequencyType.Quarterly,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2024, 1, 1)
        };

        //Act
        var investmentDto = _investmentMapper.MapToDTO(investment);

        //Assert
        Assert.Equal(investment.ID, investmentDto.ID);
        Assert.Equal(investment.Name, investmentDto.Name);
        Assert.Equal(investment.Description, investmentDto.Description);
        Assert.Equal(investment.Type, investmentDto.Type);
        Assert.Equal(investment.Quantity, investmentDto.Quantity);
        Assert.Equal(investment.Cost, investmentDto.Cost);
        Assert.Equal(investment.Recurring, investmentDto.Recurring);
        Assert.Equal(investment.Frequency, investmentDto.Frequency);
        Assert.Equal(investment.StartDate, investmentDto.StartDate);
        Assert.Equal(investment.EndDate, investmentDto.EndDate);
    }

    [Fact]
    public void MapToViewmodel_InvestmentDTO_MapsAllFields()
    {
        //Arrange
        var investmentDto = new InvestmentDto()
        {
            ID = 1,
            Name = "Investment A",
            Description = "Description A",
            Type = InvestmentType.Stocks,
            Quantity = 10,
            Cost = 100,
            Recurring = true,
            Frequency = FrequencyType.Quarterly,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2024, 1, 1)
        };

        //Act
        var investmentVm = _investmentMapper.MapToViewModel(investmentDto);

        //Assert
        Assert.Equal(investmentVm.ID, investmentDto.ID);
        Assert.Equal(investmentVm.Name, investmentDto.Name);
        Assert.Equal(investmentVm.Description, investmentDto.Description);
        Assert.Equal(investmentVm.Type, investmentDto.Type);
        Assert.Equal(investmentVm.Quantity, investmentDto.Quantity);
        Assert.Equal(investmentVm.Cost, investmentDto.Cost);
        Assert.Equal(investmentVm.Recurring, investmentDto.Recurring);
        Assert.Equal(investmentVm.Frequency, investmentDto.Frequency);
        Assert.Equal(investmentVm.StartDate, investmentDto.StartDate);
        Assert.Equal(investmentVm.EndDate, investmentDto.EndDate);
    }

    [Fact]
    public void MapToInvestmentEntity_InvestmentDTO_MapsAllFields()
    {
        //Arrange
        var investmentDto = new InvestmentDto()
        {
            Name = "Investment A",
            Description = "Description A",
            Type = InvestmentType.Stocks,
            Quantity = 10,
            Cost = 100,
            Recurring = true,
            Frequency = FrequencyType.Quarterly,
            StartDate = new DateTime(2023, 1, 1),
            EndDate = new DateTime(2024, 1, 1)
        };

        //Act
        var investment = _investmentMapper.MapToInvestmentEntity(investmentDto);

        //Assert
        Assert.Equal(investment.Name, investmentDto.Name);
        Assert.Equal(investment.Description, investmentDto.Description);
        Assert.Equal(investment.Type, investmentDto.Type);
        Assert.Equal(investment.Quantity, investmentDto.Quantity);
        Assert.Equal(investment.Cost, investmentDto.Cost);
        Assert.Equal(investment.Recurring, investmentDto.Recurring);
        Assert.Equal(investment.Frequency, investmentDto.Frequency);
        Assert.Equal(investment.StartDate, investmentDto.StartDate);
        Assert.Equal(investment.EndDate, investmentDto.EndDate);
    }

    [Fact]
    public void UpdateEntityFromDTO_InvestmentAndInvestmentDTO_MapsAllFieldsAndReturnsSameInstance()
    {
        var investment = new Investment
        {
            Name = "Old",
            Description = "OldDesc",
            Type = InvestmentType.Bonds,
            Quantity = 1,
            Cost = 10,
            Recurring = false,
            Frequency = null,
            StartDate = null,
            EndDate = null
        };

        var investmentDto = new InvestmentDto
        {
            Name = "New",
            Description = "NewDesc",
            Type = InvestmentType.Stocks,
            Quantity = 3,
            Cost = 30,
            Recurring = true,
            Frequency = FrequencyType.Weekly,
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 12, 31)
        };

        var updatedInvestment = _investmentMapper.UpdateEntityFromDTO(investment, investmentDto);
        
        Assert.Same(investment, updatedInvestment);

        Assert.Equal(investmentDto.Name, investment.Name);
        Assert.Equal(investmentDto.Description, investment.Description);
        Assert.Equal(investmentDto.Type, investment.Type);
        Assert.Equal(investmentDto.Quantity, investment.Quantity);
        Assert.Equal(investmentDto.Cost, investment.Cost);
        Assert.Equal(investmentDto.Recurring, investment.Recurring);
        Assert.Equal(investmentDto.Frequency, investment.Frequency);
        Assert.Equal(investmentDto.StartDate, investment.StartDate);
        Assert.Equal(investmentDto.EndDate, investment.EndDate);
    }

    // Null-input tests
    [Fact]
    public void MapToDTO_NullViewModel_IsHandled()
    {
        var ex = Record.Exception(() => _investmentMapper.MapToDTO((InvestmentViewModel)null!));
        Assert.True(ex == null || ex is ArgumentNullException);
    }

    [Fact]
    public void MapToDTO_NullInvestmentEntity_IsHandled()
    {
        var ex = Record.Exception(() => _investmentMapper.MapToDTO((Investment)null!));
        Assert.True(ex == null || ex is ArgumentNullException);
    }

    [Fact]
    public void MapToViewModel_NullDto_IsHandled()
    {
        var ex = Record.Exception(() => _investmentMapper.MapToViewModel((InvestmentDto)null!));
        Assert.True(ex == null || ex is ArgumentNullException);
    }

    [Fact]
    public void MapToInvestmentEntity_NullDto_IsHandled()
    {
        var ex = Record.Exception(() => _investmentMapper.MapToInvestmentEntity((InvestmentDto)null!));
        Assert.True(ex == null || ex is ArgumentNullException);
    }

    [Fact]
    public void UpdateEntityFromDTO_NullArgs_IsHandled()
    {
        var ex1 = Record.Exception(() => _investmentMapper.UpdateEntityFromDTO(null!, new InvestmentDto()));
        var ex2 = Record.Exception(() => _investmentMapper.UpdateEntityFromDTO(new Investment(), null!));
        Assert.True((ex1 == null || ex1 is ArgumentNullException) && (ex2 == null || ex2 is ArgumentNullException));
    }
}