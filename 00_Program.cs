using System;
using System.Text;
using PermutationOutput;
using System.Diagnostics;
using WindowsInput.Native;

namespace PasswordFinder
{
    partial class Program
    {
        static int i_digits = Program_permutation.min_digits;
        static string temp_PW;
        static int isCorrectPW = 0;
        static ulong number_PW = 0;


        static void Main(string[] args)
        {

            //메인
            new Program_permutation();

            Console.WriteLine("비밀번호 확인을 시작합니다.");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            //if (Program_permutation.PWList.Length == 0)
            //{
            //    Console.WriteLine("입력 가능한 비밀번호가 없습니다.");
            //    Console.WriteLine("프로그램을 종료합니다. (아무키나 누르세요)");

            //    Console.ReadKey();
            //    return;
            //}

            //비밀번호 입력값 초기화
            Check_Order_PWList();

            //비밀번호 확인 부분
            while (isCorrectPW == 0)
            {
                ////처음에 비밀번호 0이면 => 조합이 어떻게든 생길텐데, 우선 주석처리
                //if (Program_permutation.total_PW_numbers == 0 )
                //{
                //    Console.WriteLine("입력된 비밀번호가 없습니다.");
                //    break;

                //}

                //5글자보다 작을경우 바로 다음번호로 넘어감
                if(temp_PW.Length < 5)
                {
                    //다음 번호로 넘어감 + 다되면 종료
                    Check_Order_PWList();

                    continue;

                }

                if ((number_PW % 100) == 0)
                {
                    Console.WriteLine("({0}번째) 비밀번호 {1}를 확인 중 입니다.", number_PW, temp_PW);
                    Console.WriteLine("{0}초 경과", sw.Elapsed.ToString());
                }

                //Console.WriteLine("{0}을 확인 중입니다.", temp_PW);

                Try_InputPW(temp_PW);

                Check_CorrectPW(ref isCorrectPW, temp_PW);

                //다음 번호로 넘어감 + 다되면 종료
                Check_Order_PWList();

            }

            //종료
            Console.WriteLine("프로그램을 종료합니다. (아무키나 누르세요)");
            Console.ReadKey();

        }

        //Try_InputPW 초기화 시작
        static IntPtr InputPW = FindWindow(null, "배포용 문서 설정 변경 및 해제");
        static void Try_InputPW(string PW)
        {
            if (!InputPW.Equals(IntPtr.Zero))
            {
                ShowWindowAsync(InputPW, SW_SHOWNORMAL);

                SetForegroundWindow(InputPW);

                Sendkey.Keyboard.KeyDown(VirtualKeyCode.MENU);
                Sendkey.Keyboard.KeyPress(VirtualKeyCode.VK_C);
                Sendkey.Keyboard.KeyUp(VirtualKeyCode.MENU);
                Sendkey.Keyboard.TextEntry(PW);

                Sendkey.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            }
            else
            {
                Console.WriteLine("비밀번호 입력창을 찾을 수 없습니다.");
                Environment.Exit(0);

            }

        }

        //Check_CorrectPW 초기화 시작
        static StringBuilder Buff = new StringBuilder(128);
        static string Buff_string = null;
        static IntPtr WindowActive = IntPtr.Zero;
        static int count;
        static void Check_CorrectPW(ref int isCorrectPW, string PW)
        {
            count = 0;

            while (true)
            {
                WindowActive = GetForegroundWindow();
                GetWindowText(WindowActive, Buff, 128);
                Buff_string = Buff.ToString();

                if (Buff_string == "배포용 문서 설정 변경 및 해제")
                {
                    //암호창으로 안넘어가는 경우 창을 다시 확인하고,
                    //65535번 해도 안되면 메서드 종료하여 메인으로 넘어감
                    if (count++ > 65535)
                    {
                        Console.WriteLine("비밀번호 입력 창을 확인할 수 없습니다");
                        isCorrectPW = 98;
                        return;

                    }

                    continue;

                }
                else if (Buff_string == "문서 암호")
                {
                    //Console.WriteLine("{0}은(는) 비밀번호가 아닙니다.", PW);

                    Sendkey.Keyboard.KeyPress(VirtualKeyCode.RETURN);

                    //"문서 암호" 창이 없어질때까지 대기
                    while (Buff_string == "문서 암호")
                    {
                        WindowActive = GetForegroundWindow();
                        GetWindowText(WindowActive, Buff, 128);

                        Buff_string = Buff.ToString();
                    }

                    isCorrectPW = 0;

                    break;

                }
                else
                {
                    Console.WriteLine("PW is {0}", PW);

                    isCorrectPW = 1;

                    break;

                }

            }

        }

        static void Check_Order_PWList()
        {
            //다음 번호로 넘어감 + 다되면 종료
            if (Program_permutation.PWList[i_digits].MoveNext())
            {
                temp_PW = string.Join(string.Empty, Program_permutation.PWList[i_digits].Current);

                number_PW++;

            }
            else // MoveNext false일 경우
            {
                if (i_digits < Program_permutation.max_digits)
                {
                    //다음 자리수로 넘어감
                    i_digits++;
                    Program_permutation.PWList[i_digits].MoveNext();
                    temp_PW = string.Join(string.Empty, Program_permutation.PWList[i_digits].Current);
                    number_PW++;

                }
                //다 돌렸는데도 못찾으면 isCorrectPW 변경하여 루프 나가서 종료
                else if (i_digits == Program_permutation.max_digits)
                {
                    Console.WriteLine("비밀번호를 찾을 수 없습니다.");
                    isCorrectPW = 99;

                }
            }

        }
    }

}
