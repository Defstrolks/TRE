using System.IO;
using System.Collections.Generic;
using System;

namespace TRE
{
	class Program
	{
		string file;
		List<string> questions = new List<string>();
		List<string> answers = new List<string>();
		List<int> variants = new List<int>();

		void loadTest()
		{
			bool open = false;
			bool isQuestion = false;
			bool isAnswer = false;
			string b = "";
			int v = 0;
			for (int i = 0; i < file.Length; i++)
			{
				if (isQuestion && file[i] != '<' && file[i] != '>')
				{
					b += file[i];
				}
				if (isAnswer && file[i] != '<' && file[i] != '>')
				{
					b += file[i];
				}
				if(isQuestion && file[i] == '<')
				{
					isQuestion = false;
					questions.Add(b);
					variants.Add(v);
					v = 0;
					b = "";
				}
				if (isAnswer && file[i] == '<')
				{
					isAnswer = false;
					answers.Add(b);
					b = "";
				}
				if (file[i] == '<' && !open && !isQuestion && !isAnswer)
                {
					open = true;
					isQuestion = false;
					isAnswer = false;
				}
				if(open && file[i] == '>')
                {
					open = false;
					if(b == "question")
                    {
						isQuestion = true;
						b = "";
                    }
					else if (b == "variant")
					{
						isAnswer = true;
						b = "";
						v++;
					}
				}
				if(open && file[i] != '<' && file[i] != '>')
                {
					b += file[i];
                }
				if(i + 1 == file.Length)
                {
					variants.Add(v);
					answers.Add(b);
                }
			}
			variants.Remove(variants[0]);
		}
		void logQuestion(int x)
        {
			Console.WriteLine(questions[x]);
        }
		void logAnswers(int x)
		{
			Random rnd = new Random();
			List<int> rec = new List<int>();
			rec.Add(-1);
			if(x == 0)
            {
				for (int i = 0; i < variants[x]; i++)
				{
					int y = rnd.Next(variants[x]);
					bool isRec = false;
					for (int p = 0; p < rec.Count; p++)
					{
						if (rec[p] == y)
						{
							p--;
							y = rnd.Next(variants[x]);
							isRec = true;
						}
					}
					if (!isRec)
					{
						rec.Add(y);
						Console.WriteLine(answers[variants[x] - (variants[x] - y)]);
					}
					else
					{
						i--;
					}
				}
			}
			else
            {
				int k = 0;
                for (int i = 0; i < x; i++)
                {
					k += variants[i];
                }
				for (int i = 0; i < variants[x]; i++)
				{
					int y = rnd.Next(variants[x]);
					bool isRec = false;
					for (int p = 0; p < rec.Count; p++)
					{
						if (rec[p] == y)
						{
							p--;
							y = rnd.Next(variants[x]);
							isRec = true;
						}
					}
					if (!isRec)
					{
						rec.Add(y);
						Console.WriteLine(answers[k + y]);
					}
					else
					{
						i--;
					}
				}
			}
		}

		static void Main(string[] args)
		{
			Program p = new Program();
			var a = new StreamReader("Test.txt");
			p.file = a.ReadToEnd();

			p.loadTest();

			for (int i = 0; i > -1; i++)
            {
				Console.WriteLine("Сколько вопросов вам требуется?");
				int w = Convert.ToInt32(Console.ReadLine());

				if(w <= p.questions.Count)
				{
					Random rnd = new Random();
					int x = rnd.Next(w);
					List<int> rec = new List<int>();
					rec.Add(-1);
					for (int u = 0; u < w; u++)
					{
						bool isRec = false;
						for (int o = 0; o < rec.Count; o++)
						{
							if (rec[o] == x)
							{
								o--;
								x = rnd.Next(w);
								isRec = true;
							}
						}
						if (!isRec)
						{
							rec.Add(x);
							p.logQuestion(x);
							p.logAnswers(x);
						}
						else
                        {
							u--;
                        }
					}					
					break;
				}
				else
                {
					Console.WriteLine("Ошибка, введите снова");
                }
			}
			Console.ReadKey();
		}
	}
}
