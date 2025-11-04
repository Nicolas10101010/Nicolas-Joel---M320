package ch.noseryoung.blj;

public class Main {
    public static void main(String[] args) {
        System.out.println("======== SPIEL STARTET ========");
        System.out.println("You don't know what kind of place this is.\nThe only thing you know is that it's not safe here.\nFind a way out of here");
        System.out.println("Tip: Use help for showing the commands");
        System.out.println("===============================\n");

        Game game = new Game();
        game.startGame();

        System.out.println("======== SPIEL BEENDET ========");
    }
}