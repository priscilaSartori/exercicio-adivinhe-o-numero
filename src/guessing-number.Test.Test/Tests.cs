using Xunit;
using System.IO;
using System;
using Moq;
using guessing_number;
using FluentAssertions;

namespace guessing_number.Test;

[Collection("Sequential")]
public class TestReq1
{
    [Trait("Category", "1. Imprima uma mensagem de saudação")]
    [Theory(DisplayName = "Deve exibir mensagem inicial!")]
    [InlineData("---Bem-vindo ao Guessing Game--- /n Para começar, tente adivinhar o número que eu pensei, entre -100 e 100!")]
    public void TestGreet(string expected)
    {
        GuessNumber instance = new();
        var msg = instance.Greet();
        msg.Should().Be(expected);
    }
}

[Collection("Sequential")]
public class TestReq2
{
    [Trait("Category", "2. Receba a entrada da pessoa usuária e converta para Int")]
    [Theory(DisplayName = "Deve receber a entrada do usuário e converter para int")]
    [InlineData("10", 10)]
    [InlineData("20", 20)]
    [InlineData("30", 30)]
    [InlineData("-99", -99)]
    [InlineData("-45", -45)]
    [InlineData("0", 0)]
    public void TestReceiveUserInputAndConvert(string entry, int expected)
    {
        GuessNumber instance = new();
        instance.userValue.Should().Be(0);
        instance.randomValue.Should().Be(0);

        var msg = instance.ChooseNumber(entry);
        msg.Should().Be("Número escolhido!");
        instance.userValue.Should().Be(expected);
    }

    [Trait("Category", "2. Receba a entrada da pessoa usuária e converta para Int")]
    [Theory(DisplayName = "Deve retornar mensagem de errro quando entrada não for inteiro.")]
    [InlineData("1sony0")]
    [InlineData("")]
    [InlineData("teste")]
    [InlineData("alala")]
    [InlineData("trybe")]
    public void TestReceiveUserInputAndVerifyType(string entry)
    {
        GuessNumber instance = new();
        instance.userValue.Should().Be(0);
       instance.randomValue.Should().Be(0);

        var msg = instance.ChooseNumber(entry);
        msg.Should().Be("Entrada inválida! Não é um número.");
        instance.userValue.Should().Be(0);
    }

    [Trait("Category", "2. Receba a entrada da pessoa usuária e converta para Int")]
    [Theory(DisplayName = "Deve receber a entrada do usuário e garantir que está entre -100 e 100!")]
    [InlineData("1000")]
    [InlineData("-1000")]
    [InlineData("-101")]
    [InlineData("101")]
    [InlineData("9999")]
    public void TestReceiveUserInputAndVerifyRange(string entry)
    {
        GuessNumber instance = new();
        instance.userValue.Should().Be(0);
        instance.randomValue.Should().Be(0);

        var msg = instance.ChooseNumber(entry);
        msg.Should().Be("Entrada inválida! Valor não está no range.");
        instance.userValue.Should().Be(0);
    }
}

[Collection("Sequential")]
public class TestReq3
{
    [Trait("Category", "3. Gere um número aleatório")]
    [Theory(DisplayName = "Deve escolher randomicamente um número entre -100 e 100!")]
    [InlineData(-100, 100)]
    public void TestRandomlyChooseANumber(int MinimumRange, int MaximumRange)
    {
        GuessNumber instance = new();
        instance.randomValue.Should().Be(0);

        var msg = instance.RandomNumber();
        msg.Should().Be("A máquina escolheu um número de -100 à 100!");
        instance.randomValue.Should().BeInRange(MinimumRange, MaximumRange);

        msg = instance.RandomNumber();
        msg.Should().Be("A máquina escolheu um número de -100 à 100!");
        instance.randomValue.Should().BeInRange(MinimumRange, MaximumRange);

        msg = instance.RandomNumber();
        msg.Should().Be("A máquina escolheu um número de -100 à 100!");
        instance.randomValue.Should().BeInRange(MinimumRange, MaximumRange);
    }
}

[Collection("Sequential")]
public class TestReq4
{
    [Trait("Category", "4. Verifique a resposta da jogada")]
    [Theory(DisplayName = "Deve comparar a entrada do usuário em um caso MENOR")]
    [InlineData(50, 30)]
    [InlineData(50, 10)]
    [InlineData(50, -100)]
    [InlineData(50, 49)]
    [InlineData(50, 0)]
    public void TestProgramComparisonValuesLess(int mockValue, int entry)
    {
        Moq.Mock<IRandomGenerator> mock = new();
        mock.Setup(x => x.GetInt(It.IsAny<int>(), It.IsAny<int>())).Returns(mockValue);

        GuessNumber instance = new(mock.Object);
        instance.userValue.Should().Be(0);
        instance.randomValue.Should().Be(0);

        instance.userValue = entry;
        instance.userValue.Should().Be(entry);

        instance.RandomNumber();
        instance.randomValue.Should().Be(mockValue);

        var msg = instance.AnalyzePlay();
        msg.Should().Be("Tente um número MAIOR");
    }

    [Trait("Category", "4. Verifique a resposta da jogada")]
    [Theory(DisplayName = "Deve comparar a entrada do usuário em um caso MAIOR")]
    [InlineData(50, 60)]
    [InlineData(50, 90)]
    [InlineData(50, 100)]
    [InlineData(50, 51)]
    [InlineData(50, 77)]
    public void TestProgramComparisonValuesBigger(int mockValue, int entry)
    {
        Moq.Mock<IRandomGenerator> mock = new();
        mock.Setup(x => x.GetInt(It.IsAny<int>(), It.IsAny<int>())).Returns(mockValue);

        GuessNumber instance = new(mock.Object);
        instance.userValue.Should().Be(0);
        instance.randomValue.Should().Be(0);

        instance.userValue = entry;
        instance.userValue.Should().Be(entry);

        instance.RandomNumber();
        instance.randomValue.Should().Be(mockValue);

        var msg = instance.AnalyzePlay();
        msg.Should().Be("Tente um número MENOR");
    }

    [Trait("Category", "4. Verifique a resposta da jogada")]
    [Theory(DisplayName = "Deve comparar a entrada do usuário em um caso IGUAL")]
    [InlineData(50, 50)]
    [InlineData(100, 100)]
    [InlineData(-99, -99)]
    [InlineData(0, 0)]
    [InlineData(77, 77)]
    public void TestProgramComparisonValuesEqual(int mockValue, int entry)
    {
        Moq.Mock<IRandomGenerator> mock = new();
        mock.Setup(x => x.GetInt(It.IsAny<int>(), It.IsAny<int>())).Returns(mockValue);

        GuessNumber instance = new(mock.Object);
        instance.userValue.Should().Be(0);
        instance.randomValue.Should().Be(0);

        instance.userValue = entry;
        instance.userValue.Should().Be(entry);

        instance.RandomNumber();
        instance.randomValue.Should().Be(mockValue);

        var msg = instance.AnalyzePlay();
        msg.Should().Be("ACERTOU!");
    }
}

[Collection("Sequential")]
public class TestReq5
{
    [Trait("Category", "5. Adicione um limite de tentativas")]
    [Fact(DisplayName = "A variável maxAttempts deve ser definida para 5")]
    public void TestStartValueMaxAttempts()
    {
        GuessNumber instance = new();
        instance.maxAttempts.Should().Be(5);
    }

    [Trait("Category", "5. Adicione um limite de tentativas")]
    [Fact(DisplayName = "A variável currentAttempts deve ser 0 no inicio do jogo")]
    public void TestStartValueCurrentAttempts()
    {
        GuessNumber instance = new();
        instance.currentAttempts.Should().Be(0);
    }

    [Trait("Category", "5. Adicione um limite de tentativas")]
    [Theory(DisplayName = "A jogo deve retornar uma mensagem de erro caso o usuário exceda o número máximo de tentativas")]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(100)]
    public void TestMaxAttemptsExceeds(int maxAttemptsMock)
    {
        GuessNumber instance = new();
        instance.currentAttempts.Should().Be(0);
        instance.maxAttempts = maxAttemptsMock;
        instance.maxAttempts.Should().Be(maxAttemptsMock);
        instance.randomValue = 100;
        for (int i = 0; i < maxAttemptsMock; i++)
        {
            instance.currentAttempts.Should().Be(i);
            instance.ChooseNumber("1");
        }
        var result = instance.ChooseNumber("1");
        result.Should().Be("Você excedeu o número máximo de tentativas! Tente novamente.");
    }
}

[Collection("Sequential")]
public class TestReq6
{
    [Trait("Category", "6. Adicione níveis de dificuldade")]
    [Theory(DisplayName = "A função RandomNumberWithDifficult deve retornar um número aleatório com base no nível de dificuldade")]
    [InlineData(1, -100, 100)]
    [InlineData(2, -500, 500)]
    [InlineData(3, -1000, 1000)]
    public void TestRandomNumberWithDifficulty(int difficultyLevel, int minExpected, int maxExpected)
    {
        GuessNumber instance = new();
        instance.difficultyLevel.Should().Be(1);
        instance.difficultyLevel = difficultyLevel;
        var result = instance.RandomNumberWithDifficult();
        result.Should().Be($"A máquina escolheu um número de {minExpected} à {maxExpected}!");
    }
}

[Collection("Sequential")]
public class TestReq7
{
    [Trait("Category", "7. Adicione uma opção para reiniciar o jogo")]
    [Fact(DisplayName = "A função RestartGame deve reiniciar o jogo")]
    public void TestRestartGame()
    {
        GuessNumber instance = new();
        instance.currentAttempts.Should().Be(0);
        instance.maxAttempts.Should().Be(5);
        instance.gameOver.Should().BeFalse();
        instance.difficultyLevel.Should().Be(1);
        instance.userValue.Should().Be(0);
        instance.randomValue.Should().Be(0);


        instance.currentAttempts = 10;
        instance.maxAttempts = 10;
        instance.gameOver = true;
        instance.difficultyLevel = 2;
        instance.userValue = 10;
        instance.randomValue = 10;

        instance.currentAttempts.Should().Be(10);
        instance.maxAttempts.Should().Be(10);
        instance.gameOver.Should().BeTrue();
        instance.difficultyLevel.Should().Be(2);
        instance.userValue.Should().Be(10);
        instance.randomValue.Should().Be(10);

        instance.RestartGame();

        instance.currentAttempts.Should().Be(0);
        instance.maxAttempts.Should().Be(5);
        instance.gameOver.Should().BeFalse();
        instance.difficultyLevel.Should().Be(1);
        instance.userValue.Should().Be(0);
        instance.randomValue.Should().Be(0);
    }
}
