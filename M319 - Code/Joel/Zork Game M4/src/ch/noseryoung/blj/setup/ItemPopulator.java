package ch.noseryoung.blj.setup;

import ch.noseryoung.blj.core.Room;
import ch.noseryoung.blj.items.Item;
import ch.noseryoung.blj.items.ItemFactory;

// Handles populating rooms with items
public class ItemPopulator {

    public static void populateWorld(Room[] rooms) {
        populateYellowHallway(rooms[0]);
        populateStorageRoom(rooms[1]);
        populateElectricalRoom(rooms[2]);
        populateDeadEnd(rooms[3]);
        populateMaintenanceCorridor(rooms[4]);
        populateWetCarpetArea(rooms[5]);
        populateEmptyOffice(rooms[6]);
        populateExit(rooms[7]);
    }

    private static void populateYellowHallway(Room room) {
        room.addItem(ItemFactory.createSceneryItem("lights", "Buzzing fluorescent tubes cast uneven light across the yellowed walls"));
        room.addItem(ItemFactory.createSceneryItem("note", "A crumpled warning from a previous wanderer: 'Find the three cards. Fix the power. Get out.'"));
        room.addItem(ItemFactory.createSceneryItem("walls", "Endless yellow wallpaper peels at the edges, stained with age"));
    }

    private static void populateStorageRoom(Room room) {
        room.addItem(ItemFactory.createSceneryItem("boxes", "Dusty containers filled with forgotten junk and old supplies"));
        room.addItem(ItemFactory.createUsableItem("flashlight", "A small but functional flashlight with a sturdy grip (Could be useful revealing objects which lay in the dark)", "reveals hidden items"));
        room.addItem(ItemFactory.createSceneryItem("shelves", "Metal shelving units tilting under the weight of old boxes"));
    }

    private static void populateElectricalRoom(Room room) {
        room.addItem(ItemFactory.createSceneryItem("panel", "An old control panel with blinking red and green lights"));
        room.addItem(ItemFactory.createSceneryItem("electrical", "An old control panel with blinking red and green lights"));
        room.addItem(ItemFactory.createUsableItem("battery", "A heavy-duty battery pack still showing charge indicators", "restores power"));
        room.addItem(ItemFactory.createSceneryItem("pipes", "Exposed metal pipes running along the ceiling, some leaking"));
        room.addItem(ItemFactory.createSceneryItem("signs", "Faded warning signs hanging crooked on rusted surfaces"));
    }

    private static void populateDeadEnd(Room room) {
        room.addItem(ItemFactory.createSceneryItem("scratches", "Deep claw marks gouged into the yellow wall, as if made in desperation"));
        room.addItem(ItemFactory.createSceneryItem("wall", "A blank yellow wall that blocks any further progress"));

        Item redKeycard = ItemFactory.createUsableItem("red-keycard", "A red access card with faded text reading 'SECURITY LEVEL 1'", "unlocks exits");
        redKeycard.setHidden(true);
        room.addItem(redKeycard);
    }

    private static void populateMaintenanceCorridor(Room room) {
        room.addItem(ItemFactory.createSceneryItem("pipes", "Rusty overhead pipes dripping condensation steadily"));
        room.addItem(ItemFactory.createSceneryItem("vents", "Large ventilation ducts covered in dust and grime"));

        Item blueKeycard = ItemFactory.createUsableItem("blue-keycard", "A blue access card, slightly bent but still functional", "unlocks exits");
        blueKeycard.setHidden(true);
        room.addItem(blueKeycard);

        room.addItem(ItemFactory.createSceneryItem("tools", "Scattered maintenance tools covered in rust and grime"));
    }

    private static void populateWetCarpetArea(Room room) {
        room.addItem(ItemFactory.createSceneryItem("carpet", "Thoroughly soaked carpet that squelches dangerously underfoot"));
        room.addItem(ItemFactory.createSceneryItem("mold", "Dark stains climbing the walls like grasping fingers"));

        Item greenKeycard = ItemFactory.createUsableItem("green-keycard", "A green access card, water-damaged but still readable", "unlocks exits");
        greenKeycard.setHidden(true);
        room.addItem(greenKeycard);

        room.addItem(ItemFactory.createSceneryItem("stains", "Dark water damage spreading across walls and ceiling"));
    }

    private static void populateEmptyOffice(Room room) {
        room.addItem(ItemFactory.createSceneryItem("desk", "A metal desk covered in yellowed papers and old coffee stains"));
        room.addItem(ItemFactory.createSceneryItem("chair", "A broken office chair with torn padding spilling out"));
        room.addItem(ItemFactory.createSceneryItem("papers", "Yellowed documents scattered across the floor, too faded to read"));
        room.addItem(ItemFactory.createUsableItem("water", "Half-full plastic bottle, still sealed and clean", "consumable"));
        room.addItem(ItemFactory.createSceneryItem("window", "A cracked window revealing only impenetrable darkness beyond"));
        room.addItem(ItemFactory.createSceneryItem("reader", "A security panel mounted on the wall with three empty card slots"));
    }

    private static void populateExit(Room room) {
        room.addItem(ItemFactory.createSceneryItem("reader", "A security panel with three card slots, all currently empty"));
        room.addItem(ItemFactory.createSceneryItem("door", "Heavy metal door marked 'EMERGENCY EXIT' - your only way out"));
        room.addItem(ItemFactory.createSceneryItem("locks", "Multiple electronic locks securing the exit door"));
        room.addItem(ItemFactory.createSceneryItem("light", "A blinking red light indicating the door remains sealed"));
    }
}