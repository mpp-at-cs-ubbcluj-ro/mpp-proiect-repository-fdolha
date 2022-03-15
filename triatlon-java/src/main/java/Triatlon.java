import model.person.Athlete;
import repository.AthleteRepository;
import repository.db.AthleteDBRepository;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class Triatlon {
    public static void main(String[] args) {
        Properties properties = new Properties();
        try {
            properties.load(new FileReader("bd.config"));
        } catch (IOException ignored) {}
        AthleteRepository athleteRepository = new AthleteDBRepository(properties);
        athleteRepository.save(new Athlete("Flavius", "Dolha"));
        System.out.println("Toti din DB:");
        for (var athlete : athleteRepository.findAll()) System.out.println(athlete);
    }
}