import java.io.IOException;
import java.net.UnknownHostException;
import java.util.Scanner;

public class ConsoleChat {
	private static String _nick;
	private static String _ipLocal;
	private static String _ipDestiny;

	// portas devem ser diferentes (por enquanto)
	private static String _portLocal;
	private static String _portDestiny;

	static SoketSupport _soketClient = new SoketSupport();
	static SoketSupport _soketServer = new SoketSupport();

	static Scanner input = new Scanner(System.in);

	public static void main(String[] args) {

		System.out.print("Digite seu nick:");

		_nick = input.nextLine();

		System.out.println("Pegando seu IP... ");
		try {
			String[] ips = _soketClient.getLocalIp();
			System.out.println("selecione um dos ips abaixo:\n");
			int count = 0;
			for (String string : ips) {
				System.out.println("[" + count + "] - " + string);
				count++;
			}
			int i = input.nextInt();
			_ipLocal = ips[i];

		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		input.nextLine();
		System.out.println(_ipLocal);
		System.out.println("Informe o IP da outra ponta:");
		_ipDestiny = input.nextLine();

		System.out.println("Informe a Porta Local...");
		_portLocal = input.nextLine();

		System.out.println("Informe a Porta da outra ponta:");
		_portDestiny = input.nextLine();

		System.out.println("Iniciando Threads..");
		Runnable stask = new serverStart();
		Thread server = new Thread(stask);

		Runnable ctask = new clientStart();
		Thread client = new Thread(ctask);

		System.out.println("Startando Threads..");
		server.start();
		client.start();

		System.out.println("Pronto para digitar.");

		// Threads assumem

	}

	private static class serverStart implements Runnable {
		// server recebe requisição e responde
		@Override
		public void run() {
			try {
				_soketServer.configure(As.server, _ipLocal, _portLocal);
			} catch (IOException ioe) {
				// TODO Auto-generated catch block
				ioe.printStackTrace();
			}

			while (true) {
				try {
					// server aguardando requisição
					String texto = _soketServer.receiveData();
					System.out.println(texto);

					_soketServer.ForceClose();
					// não precisa retornar, apenas fecha coneção
					// caso contrario var response =
					// _soketServer.SendData("Resposta");
				} catch (Exception e) {
					System.out.println("EXCEPTION: " + e.getMessage());
				}
			}
		}
	}

	static class clientStart implements Runnable {

		@Override
		public void run() {
			// TODO Auto-generated method stub
			// cliente envia requisição e aguarda resposta
			try {
				_soketClient.configure(As.client, _ipDestiny, _portDestiny);
			} catch (IOException ioe) {
				// TODO Auto-generated catch block
				ioe.printStackTrace();
			}

			while (true) {
				try {
					String texto = input.nextLine();

					_soketClient.sendData(_nick + " : " + texto);

					// não espera receber, apenas fecha coneção
					_soketClient.ForceClose();
				} catch (Exception e) {
					System.out.println("EXCEPTION: " + e.getMessage());
				}
			}
		}

	}
}
