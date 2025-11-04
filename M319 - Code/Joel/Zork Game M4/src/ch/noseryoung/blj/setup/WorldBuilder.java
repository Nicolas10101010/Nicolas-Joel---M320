package ch.noseryoung.blj.setup;

import ch.noseryoung.blj.core.Room;

// Handles room creation and connections
public class WorldBuilder {

    public static Room[] createWorld() {
        Room[] rooms = createRooms();
        connectRooms(rooms);
        return rooms;
    }

    private static Room[] createRooms() {
        Room yellowHallway = new Room("Yellow Hallway",
                "An endless corridor stretches before you. Yellowed wallpaper peels at the edges, and fluorescent lights buzz overhead, casting a sickly glow.\n" +
                        "The worn carpet squelches slightly under your feet. The air smells of old moisture and something indefinable.\n" +
                        "To the north, a doorway leads to what appears to be a storage area. To the south, the corridor continues toward a maintenance section.",
                null, null, null, null);

        Room storageRoom = new Room("Storage Room",
                "A cramped room filled with dusty cardboard boxes and forgotten supplies. Shelves line the walls, some tilting precariously.\n" +
                        "A single bulb dangles from the ceiling, casting long shadows between the stacked containers. Everything here feels abandoned for decades.\n" +
                        "The hallway lies to the south. To the north, the corridor ends abruptly. An opening to the east reveals humming electrical equipment.",
                null, null, null, null);

        Room electricalRoom = new Room("Electrical Room",
                "The constant hum of machinery fills this small technical space. Exposed pipes run along the ceiling, and an old electrical panel dominates one wall.\n" +
                        "Warning signs in faded text hang crooked on rusted metal surfaces. The air is warm and electric.\n" +
                        "The storage room lies to the west.",
                null, null, null, null);

        Room deadEnd = new Room("Dead End",
                "The hallway abruptly terminates at a blank yellow wall. A fluorescent light above flickers erratically, creating an unsettling strobe effect.\n" +
                        "Deep scratches mark the wall, as if someone tried desperately to claw their way through. The only escape is back south toward the storage room.",
                null, null, null, null);

        Room maintenanceCorridor = new Room("Maintenance Corridor",
                "A narrow service tunnel with exposed pipes and ventilation ducts overhead. Water drips steadily from somewhere unseen, echoing in the confined space.\n" +
                        "Tool marks and rust stains suggest this area sees occasional, reluctant maintenance.\n" +
                        "The main hallway is to the north. To the west, you see an abandoned office space. To the east, you glimpse a wet, deteriorating area.",
                null, null, null, null);

        Room wetCarpetArea = new Room("Wet Carpet Area",
                "The carpet here is thoroughly soaked, squelching with each step. Water stains climb the walls like dark fingers, and the air is thick with the smell of mold and decay.\n" +
                        "You sense this place might be dangerous to linger in. The maintenance corridor offers the only exit to the west.",
                null, null, null, null);

        Room emptyOffice = new Room("Empty Office",
                "A forgotten office space with a metal desk and broken office chair. Papers yellow with age are scattered across the floor.\n" +
                        "A cracked window reveals only darkness beyond. The place feels like it was abandoned mid-workday, years ago.\n" +
                        "The maintenance corridor stretches to the east. To the south, a heavy door marked 'EXIT' catches your attention.",
                null, null, null, null);

        Room exit = new Room("Exit",
                "A heavy metal door marked 'EMERGENCY EXIT' blocks your path. Multiple locks and card readers suggest this is the way out, but it remains firmly sealed.\n" +
                        "A red light blinks ominously on the access panel. The office lies to the north, your only current option for retreat.",
                null, null, null, null);

        return new Room[]{yellowHallway, storageRoom, electricalRoom, deadEnd,
                maintenanceCorridor, wetCarpetArea, emptyOffice, exit};
    }

    private static void connectRooms(Room[] rooms) {
        Room yellowHallway = rooms[0];
        Room storageRoom = rooms[1];
        Room electricalRoom = rooms[2];
        Room deadEnd = rooms[3];
        Room maintenanceCorridor = rooms[4];
        Room wetCarpetArea = rooms[5];
        Room emptyOffice = rooms[6];
        Room exit = rooms[7];

        yellowHallway.setNorthRoom(storageRoom);
        storageRoom.setSouthRoom(yellowHallway);

        storageRoom.setNorthRoom(deadEnd);
        deadEnd.setSouthRoom(storageRoom);

        storageRoom.setEastRoom(electricalRoom);
        electricalRoom.setWestRoom(storageRoom);

        yellowHallway.setSouthRoom(maintenanceCorridor);
        maintenanceCorridor.setNorthRoom(yellowHallway);

        maintenanceCorridor.setEastRoom(wetCarpetArea);
        wetCarpetArea.setWestRoom(maintenanceCorridor);

        maintenanceCorridor.setWestRoom(emptyOffice);
        emptyOffice.setEastRoom(maintenanceCorridor);

        emptyOffice.setSouthRoom(exit);
        exit.setNorthRoom(emptyOffice);
    }
}