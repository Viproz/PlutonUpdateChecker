using System;

namespace UpdateChecker {
    class MainClass {
        public static int Main(string[] args) {
            Console.WriteLine("Hello World!");
			ServerTester tester = new ServerTester();
            Console.ReadKey();

            return 1;
        }
    }
}
