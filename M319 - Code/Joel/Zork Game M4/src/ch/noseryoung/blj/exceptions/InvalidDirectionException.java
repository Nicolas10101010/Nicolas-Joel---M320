package ch.noseryoung.blj.exceptions;

public class InvalidDirectionException extends Exception {
    public InvalidDirectionException(String direction) {
        super("Cannot move " + direction + " - there's a wall in that direction");
    }
}