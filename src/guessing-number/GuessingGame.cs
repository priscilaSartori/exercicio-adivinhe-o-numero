using System;

namespace guessing_number;

public class GuessNumber
{
  //In this way we are passing the random number generator by dependency injection
  private IRandomGenerator random;
  public GuessNumber() : this(new DefaultRandom()) { }
  public GuessNumber(IRandomGenerator obj)
  {
    this.random = obj;

    userValue = 0;
    randomValue = 0;
  }

  //user variables
  public int userValue;
  public int randomValue;

  public int maxAttempts = 5;
  public int currentAttempts = 0;

  public int difficultyLevel;

  public bool gameOver;

  //1 - Imprima uma mensagem de saudação
  public string Greet()
  {
    string mensagem1 = "---Bem-vindo ao Guessing Game--- /n Para começar, tente adivinhar o número que eu pensei, entre -100 e 100!";
    return mensagem1;
  }

  //2 - Receba a entrada da pessoa usuária e converta para Int
  //5 - Adicione um limite de tentativas

  public string ChooseNumber(string userEntry)
  {
    currentAttempts++;
    if (currentAttempts > maxAttempts)
    {
      gameOver = true;
      return "Você excedeu o número máximo de tentativas! Tente novamente.";
    }

    bool canConvert = Int32.TryParse(userEntry, out userValue);
    if (!canConvert)
    {
      return "Entrada inválida! Não é um número.";
    }

    if (userValue > 100 || userValue < -100)
    {
      userValue = 0;
      return "Entrada inválida! Valor não está no range.";
    }
    return "Número escolhido!";
  }

  //3 - Gere um número aleatório
  public string RandomNumber()
  {
    randomValue = random.GetInt(-100, 100);
    return "A máquina escolheu um número de -100 à 100!";
  }

  //6 - Adicione níveis de dificuldade
  public string RandomNumberWithDifficult()
  {
    throw new NotImplementedException();
  }

  //4 - Verifique a resposta da jogada
  public string AnalyzePlay()
  {
    if (userValue > randomValue)
      return "Tente um número MENOR";
    else if (userValue < randomValue)
      return "Tente um número MAIOR";
    else
    {
      gameOver = true;
      return "ACERTOU!";
    }
  }

  //7 - Adicione uma opção para reiniciar o jogo
  public void RestartGame()
  {
    throw new NotImplementedException();
  }
}