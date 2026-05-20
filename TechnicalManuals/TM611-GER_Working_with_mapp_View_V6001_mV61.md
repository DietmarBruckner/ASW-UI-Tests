## Page 1

TM611

Arbeiten mit mapp View

## Page 2

2 ARBEITEN MIT MAPP VIEW TM611
Voraussetzungen
Abgeschlossene Trai-
SEM210 Automation Studio Training: Basics
nings
Automation Studio 6.0.2
Software Automation Runtime 6.0
mapp View 6.0.0
Hardware ARsim

## Page 3

INHALTSVERZEICHNIS 3
Inhaltsverzeichnis
1 Einleitung..............................................................................................................................................................5
1.1 Lernziele..................................................................................................................................................5
1.2 Sicherheitshinweise und Symbole....................................................................................................5
1.3 B&R Onlinekurse...................................................................................................................................6
2 Konzept mapp View...........................................................................................................................................7
2.1 Installation mapp View Technology Package.................................................................................8
2.2 Lizenzierung..........................................................................................................................................8
2.3 Organisation der Visualisierung.......................................................................................................9
3 mapp View Authentifizierung........................................................................................................................10
3.1 OPC-UA-Server Konfiguration..........................................................................................................10
3.2 mapp View Server Konfiguration....................................................................................................12
4 mapp View Visualisierungsvorlagen.............................................................................................................13
4.1 .vis-Datei...............................................................................................................................................15
4.2 Visualisierung im Browser................................................................................................................16
5 Seitenerstellung................................................................................................................................................18
5.1 Layout...................................................................................................................................................19
5.2 Area.......................................................................................................................................................20
5.3 Page......................................................................................................................................................22
5.4 Content & Widgets............................................................................................................................23
6 Navigation..........................................................................................................................................................27
6.1 Manuelle Navigation..........................................................................................................................27
6.2 Automatische Navigation.................................................................................................................27
6.3 Manuelle Navigation anwenden......................................................................................................28
6.4 NavigationBar-Widget......................................................................................................................29
6.5 NavigationButton-Widget................................................................................................................30
7 Optische Gestaltung - Styling.......................................................................................................................32
7.1 Styleable Property..............................................................................................................................32
7.2 Style.......................................................................................................................................................33
7.3 Theme...................................................................................................................................................33
8 Datenanbindung...............................................................................................................................................36
8.1 Binding.................................................................................................................................................36
8.2 OPC UA.................................................................................................................................................37
8.3 Datenanbindung anwenden............................................................................................................38
9 Mediendateien..................................................................................................................................................45
9.1 Image-Widget.....................................................................................................................................45
9.2 SVG Symbol Bibliothek.....................................................................................................................47
10 Benutzer-Rollen-System................................................................................................................................48
10.1 RBAC-Konzept...................................................................................................................................48
10.2 Benutzer-Rollen-System anwenden..............................................................................................49
10.3 Schreibrechte auf OPC UA Node einschränken.........................................................................49
10.4 Authentifizierung in der Visualisierung.......................................................................................51

## Page 4

4 ARBEITEN MIT MAPP VIEW TM611
11 Lokalisierung...................................................................................................................................................54
11.1 Textsystem.........................................................................................................................................54
11.2 Identifier.............................................................................................................................................55
11.3 Konfiguration der Projektsprachen..............................................................................................55
11.4 Konfiguration der Textsystem-Konfigurationsdatei................................................................55
11.5 Textdateien in mapp View.............................................................................................................56
11.6 Textsystem anwenden....................................................................................................................56
11.7 Einheitensystem...............................................................................................................................60
11.8 Einheitensystem anwenden...........................................................................................................60
12 Ereignisse und Aktionen...............................................................................................................................64
12.1 Ereignisse...........................................................................................................................................64
12.2 Aktionen.............................................................................................................................................65
12.3 Operanden.........................................................................................................................................66
12.4 Ereignisse, Aktionen und Operanden anwenden......................................................................67
12.5 Wert setzen........................................................................................................................................67
13 Zusammenfassung.........................................................................................................................................74

## Page 5

EINLEITUNG 5
1 Einleitung
mapp Technology
mapp View gehört zum Software-Paket mapp Technology. Mit den vorkon-
figurierten, modularen Software-Bausteinen reduzieren Sie den Program-
mieraufwand für Ihre Automatisierungssoftware wesentlich. Die intelligen-
Abbildung 1: B&R mapp Technology Logo
ten "mapps" verknüpfen sich automatisch miteinander, sodass diese Pro-
grammieraufwände ebenfalls erheblich sinken. Das Portfolio erweitert sich
ständig.
mapp View
Mit mapp View kann jeder Automatisierungstechniker ohne großen Aufwand
übersichtliche Web-Visualisierungsseiten gestalten. Die Auseinandersetzung
mit Web-Technologien ist dabei nicht notwendig. B&R Widgets kapseln Web-
Abbildung 2: B&R mapp View Logo
Technologien und werden per Drag-and-drop auf die gewünschte Seite gezo-
gen und dort einfach parametriert.
1.1 Lernziele
In diesem Trainingmodul werden die ersten Schritte einer Visualisierung gezeigt. Anhand der Aufgaben werden typi-
sche Anwendungsfälle gezeigt und dazu erforderliche Funktionen erklärt.
Nach diesem Trainingsmodul sind Sie in der Lage:
das Konzept und die Eigenschaften von mapp View zu beschreiben.
•
eine neue mapp View Visualisierung in ein Automation Studio Projekt einzufügen und zu konfigurieren.
•
Layouts zu erstellen und damit Visualisierungsseiten zu organisieren.
•
Widgets zu verwenden, um die Inhalte einer mapp View Visualisierung zu gestalten.
•
die beiden Navigationsarten zu unterscheiden und die manuelle Navigation zu implementieren.
•
Prozessdaten aus der Applikation in einer mapp View Visualisierung einzubinden.
•
Bilder für die visuelle Gestaltung in einer mapp View Visualisierung einzusetzen.
•
das Benutzer-Rollen-System zu verwenden, um die Eigenschaften einer mapp View Visualisierung abhängig vom
•
angemeldeten Benutzer zu konfigurieren.
das Textsystem mit Sprachumschaltung und das Einheitensystem mit Einheitenumrechnung zu implementieren.
•
die Verbindung von auslösendem Ereignis und darauffolgenden Aktion zu implementieren.
•
1.2 Sicherheitshinweise und Symbole
Die Sicherheitshinweise werden im vorliegenden Handbuch wie folgt gestaltet:
Gefahr: Bei Missachtung der Sicherheitsvorschriften und -hinweise besteht die Gefahr schwerer Verlet-
zungen, Todesgefahr oder großer Sachschäden.
Warnung: Bei Missachtung der Sicherheitsvorschriften und -hinweise besteht die Gefahr schwerer Ver-
letzungen oder großer Sachschäden.
Vorsicht: Bei Missachtung der Sicherheitsvorschriften und -hinweise besteht die Gefahr von Verletzun-
gen oder von Sachschäden. Wichtige Angaben zur Vermeidung von Fehlfunktionen.
Hinweise und Zusatzinformationen werden im vorliegenden Handbuch wie folgt gestaltet:
Hinweis: Hier werden wichtige Hinweise und Zusatzinformationen bereitgestellt.

## Page 6

6 ARBEITEN MIT MAPP VIEW TM611
Hilfe: Hier wird auf ein Eintrag von Automation Help verwiesen, der weiterführende Informationen, Da-
tenblätter oder Anwenderhandbücher enthält.
Beispiel: Programmierung \ Variablen und Datentypen \ Datentypen \ Einfache Datentypen
Mit Klick auf den Link wird Automation Help geöffnet.
Beispiel: Hier wird eine beispielhafte Darstellung gezeigt, die das Gelernte vertieft.
Resultat: Hier wird das Ergebnis einer vorangegangenen Aufgabenstellung kurz zusammengefasst.
Gestaltung der Sicherheitshinweise in externen Handbüchern:
In diesem Handbuch wird auf andere Handbücher verwiesen. Die Gestaltung der Sicherheitshinweise ist im jeweiligen
externen Handbuch beschrieben.
Aufgabe: Aufgabenstellungen & Übungsaufgaben
In den grau hinterlegten Abschnitten sind Aufgabenstellungen sowie die zugehörigen Handlungsschritte beschrieben.
Die Aufgabenstellungen dienen zur Vertiefung der bereitgestellten Informationen.
1.3 B&R Onlinekurse
Die B&R Onlinekurse stellen Lerneinheiten für verschiedenste Themen zur Verfügung. Durch
die Interaktivität der Kurse wird ein effektives Lernen von Inhalten ermöglicht.
Um die benötigten B&R Onlinekurse schneller zu finden, sind die verschiedenen Kurse auf der
Webseite den unterschiedlichen Trainingskategorien zugeordnet und orientieren sich am
modularen Trainingskonzept.
B&R Onlinekurse (https://www.br-automation.com/de/academy/virtuelles-klassenzim-
mer/onlinekurse/)

## Page 7

KONZEPT MAPP VIEW7

2Konzept mapp View

Plattformunabhängig

Abbildung 3: mapp View ist plattformunabhängig

mapp View basiert auf den weltweit verwendeten Web-Standards HTML5, CSS3 und Javascript. Im Gegensatz zu pro-

prietären Plattformen werden diese Standards auf Jahrzehnte hinaus nutzbar bleiben und weiterentwickelt werden.

Das schafft Investitionssicherheit.

Nahtlos integriert

Abbildung 4: mapp View ist nahtlos integriert

Die Projektierung erfolgt in der B&R-Automatisierungssoftware Automation Studio. Die Verwaltung der Seiten und

anderer Ressourcen erfolgt in der logischen Ansicht. Die Toolbox stellt dabei alle Widgets übersichtlich zur Verfügung.

## Page 8

8ARBEITEN MIT MAPP VIEW TM611

2.1Installation mapp View Technology Package

Um mapp View verwenden zu können, wird Automation Studio benötigt.

Die gewünschte Version des mapp View Technology Packages kann über die B&R Website oder über den Upgrade-Dia-

log heruntergeladen werden. Die Installation erfolgt im Upgrade-Dialog in Automation Studio.

Bei der Installation des Technology Packages wird automatisch die Automation Help um die neuen Informationen (von

mapp View) erweitert.

Abbildung 5: Automation Studio Upgrade-Dialog \ Technology Packages

2.2Lizenzierung

Eine mapp View Visualisierung kann für Tests bzw. während der Entwicklung auf der ARsim ohne Einschränkungen und

Lizenzen verwendet werden. Für hardwarebasierte Zielsysteme ist eine Lizenz notwendig.

LizenzmodellBestellnummerFunktionsumfang

mapp View Starter1TCMPVIEW.00-01Erlaubt die Verwendung eines mapp View Clients, sowie

eines OPC-UA-Servers. Des Weiteren sind alle Basis Wid-

gets enthalten.

Die Einschränkungen der Widgets wird durch entspre-

chende Symbole im Widget Katalog bei geöffnetem Con-

tent Editor angezeigt.

mapp View Premium - Widgets1TCMPVIEWWGT.10-01Erlaubt die Verwendung aller Widgets, welche in der frei-

en Version nicht enthalten sind, wie z. B. ContentCarou-

sel, FlexBox, VNCViewer, WebViewer.

mapp View Premium - Clients1TCMPVIEWCLT.10-01Erlaubt die Verwendung beliebiger mapp View Clients.

mapp View Premium - Server1TCMPVIEWSRV.10-01Erlaubt die Anbindung an mehr als einen OPC-UA-Server.

mapp View Ultimate1TCMPVIEW.20-01Erlaubt die Verwendung aller Widgets, einer beliebigen

Anzahl von mapp View Clients und die Anbindung an

mehr als einen OPC-UA-Server.

Tabelle 1: B&R Paketbasiertes Lizenzmodell

Visualization \ mapp View \ General information \ Licensing

## Page 9

KONZEPT MAPP VIEW9

2.3Organisation der Visualisierung

Die Elemente einer mapp View Visualisierung werden in Automation Studio projektiert und verwaltet.

In der Logical View erfolgt die Verwaltung der Visualisierungsseiten, der Texte sowie der Grafikdateien.

In der Configuration View wird aus den Elementen der Logical View eine oder mehrere Visualisierungen in der aktiven

Konfiguration "zusammengebaut". Somit ist eine klare Trennung zwischen den Quelldateien von Visualisierungsele-

menten und der für eine Maschinenkonfiguration notwendigen HMI möglich.

Abbildung 6: Organisation der Visualisierung in Automation Studio

Logical View

In der Logical View werden Elemente einer Visualisierung (Pages, Contents, Texte, Mediadateien, etc.) in einem mapp

View Package verwaltet.

Configuration View

In der Configuration View werden eine oder mehrere Visualisierungen verwaltet und konfiguriert. Außerdem werden

Binding-Informationen zwischen einem Widget und einer Variable hier verwaltet.

Page / Content Editor

Grafischer Editor zum Designen einer Page oder deren Teilbereiche (Content). Die Bearbeitung eines Contents kann

sowohl in grafischer als auch textueller Form (XML) erfolgen.

Toolbar

In der Toolbar werden im grafischen Editor Werkzeuge für das Bearbeiten von Widgets im grafischen Designer zur

Verfügung gestellt.

Widget Katalog

Ist ein Content geöffnet (grafisch oder textuell), können Widgets aus dem Katalog auf die Zeichenfläche des Content

Editors eingefügt und projektiert werden.

Property Editor

Elemente einer Visualisierung werden über den Property Editor konfiguriert. Je nach Eigenschaft, z. B. eines Widgets,

stehen unterschiedliche Dialoge für die Bearbeitung zur Verfügung.

## Page 10

10ARBEITEN MIT MAPP VIEW TM611

3mapp View Authentifizierung

In einer typischen mapp View Umgebung läuft der mapp View Server auf einer Steuerung.

Dieser mapp View Server hat eine Verbindung zu einem lokal laufenden OPC-UA-Server, um Daten der Steuerung zu

erhalten. Gleichzeitig kann der mapp View Server auch noch weitere Verbindungen zu Remote OPC-UA-Server halten.

Auf den mapp View Server greifen ein oder mehrere Webbrowser zu, um die eigentliche Visualisierung darzustellen.

Abbildung 7: Schematische Darstellung der Kommunikation

Bei der Authentifizierung von Benutzern wird überprüft, ob diese tatsächlich die sind, für die sie sich

ausgeben.

Die Authentifizierung ist ein entscheidender Bestandteil der Cybersicherheit und ermöglicht es einem

Unternehmen, den Zugriff auf die Systeme und Daten zu kontrollieren.

3.1OPC-UA-Server Konfiguration

Die Verbindung zwischen mapp View Server und OPC UA erfolgt über das OPC-UA-Protokoll, ohne Signierung und Ver-

schlüsselung, auf Port 4840.

Communication \ OPC UA C/S \ Configuration in AS \ OPC UA C/S configuration

In den ersten Aufgaben erfolgt die Kommunikation zwischen mapp View Client (Browser) und dem mapp

View Server (Steuerung) über das http Protokoll (Default Port 81) und die Authentifizierung ohne Benut-

zeranmeldedaten, da noch keine Benutzer und Rollen im Projekt verfügbar sind.

Die Benutzer und Rollen werden zu einem späteren Zeitpunkt erstellt. Ab diesem Zeitpunkt wird die Au-

thentifizierung geändert.

Für die OPC-UA-Kommunikation wird zudem vorausgesetzt, dass der Zugang zu OPC UA über das Anonymous Token

Zugang erlaubt ist und dass das Anonymous Token in den Rollen zum Lesen und auch Schreiben von Daten erlaubt

ist. In der Standardkonfiguration ist entsprechend das Anonymous Token der Rolle BR_Observer und BR_Engineer

zuzuweisen!

## Page 11

MAPP VIEW AUTHENTIFIZIERUNG11

Aufgabe: OPC-UA-Server für eine Anmeldung ohne Benutzeranmeldedaten konfigurieren

Ziel der ersten Aufgabe ist es auf Basis des Automation Studio Projektes "mappViewGettingStarted" den OPC-UA-

Server zu aktivieren und eine Authentifizierung am OPC-UA-Server ohne Benutzeranmeldedaten zu erlauben.

Das Projekt steht im Training als .zip-Datei zur Verfügung und dient als Basis für die Erstellung der mapp View Visua-

lisierung.

1)"mappViewGettingStarted" Projekt öffnen

2)Ggf. über den Upgrade-Dialog das mapp View Paket herunterladen und installieren.

Anschließend die mapp View Version im Projekt einstellen

3)In der Configuration View unter "Connectivity" die Datei "UaCsConfig.uacfg" öffnen

a)Erweiterte Darstellung akivieren

b)OPC-UA-Server aktivieren

c)Anonymen Zugriff aktivieren

d)Rolle "BR_Engineer" zuweisen, um auf OPC-UA-Variablen schreiben zu können

Ergebnis:

Der OPC-UA-Server wurde für den anonymen Zugang projektiert.

Abbildung 8: Der OPC-UA-Server wurde aktiviert und für eine anonyme Authentifizierung projektiert

## Page 12

12ARBEITEN MIT MAPP VIEW TM611

3.2mapp View Server Konfiguration

In der mapp View Server Konfiguration werden statische Einstellungen für den mapp View Server, das OPC-UA-System

und der mapp View Clients projektiert.

In der Standardkonfiguration erfolgt die Kommunikation zwischen dem mapp View Server und den mapp View Clienten

(Browsern) über das HTTPS (Default Port 443) Protokoll.

Beim Aufruf der Visualisierung wird der Benutzer zur Eingabe eines Benutzernamens und eines Passworts aufgeordert.

Visualization \ mapp View \ Engineering \ Organization of the HMI application \ mapp View configuration

\ Server configuration

Während der Entwicklung der Visualisierung kann die Kommunikation über das http Protokoll mit einer

anonymen Authentifizierung durchgeführt werden.

Der Datenaustausch zwischen mapp View und Browser erfolgt in diesem Fall nicht verschlüsselt - der

Datenaustausch kann prinzipiell von jedem mitgelesen werden.

Für die finale Auslieferung der Visualisierung in eine Produktivumgebung ist jedoch eine https Kommu-

(erfordert ein offiziell erstelltes bzw. signiertes Zertifikat, da ansonstennikation dringend empfohlen

die Datenkommunikation nicht sicher ist)! Die Authentifizierung ist über einen projektierten Benutzer-

namen mit dessen Passwort durchzuführen.

Visualization \ mapp View \ Engineering \ Organization of the HMI application \ mapp View configuration

\ Server configuration \ Secure mapp View HMI applications

Aufgabe: mapp View Konfiguration einfügen und die Server Einstellungen projektieren

Ziel dieser Aufgabe ist es, eine mapp View Konfiguration in das Projekt einzufügen und die mapp View Server Einstel-

lungen für eine Authentifizierung ohne Benutzeranmeldedaten und einer HTTP Kommunikation zu ändern.

1)In der Configuration View den Knoten "mapp View" auswählen

2)Aus dem Objektkatalog die mapp View Konfiguration ("Config.mappviewcfg") einfügen und öffnen

a)Das Protokoll "HTTP" auswählen

b)Als Statup Benutzer "anonymous token" auswählen

Ergebnis:

Eine mapp View Visualisierung kann sich ohne Benutzeranmeldedaten am mapp View Server über das

http Protokoll authentifizieren.

Abbildung 9: Das Protokoll wurde auf HTTP verändert, der Startup Benutzer auf "anonymous token"

## Page 13

MAPP VIEW VISUALISIERUNGSVORLAGEN13

4mapp View Visualisierungsvorlagen

Dem Anwender stehen für den schnellen mapp View Einstig Visualisierungsvorlagen zur Verfügung. Diese haben un-

terschiedliche Orientierungen (Portrait, Landscape) und Auflösungen. Vorlagen können sowohl in ein bestehendes als

auch in ein neues Automation Studio Projekt eingefügt werden.

Jede Vorlage enthält alle für eine mapp View Visualisierung notwendigen Elemente und kann vom Anwender beliebig

erweitert werden.

Abbildung 10: Dialog zur Auswahl einer mapp View Vorlage

Visualization \ mapp View \ Engineering \ mapp View visualization templates

Aufgabe: Einfügen einer mapp View Visualisierung

Eine mapp View Visualisierung wird in der Logical View über den Objektkatalog eingefügt. Im Auswahldialog stehen

Vorlagen für unterschiedliche Displayauflösungen und Orientierungen zur Verfügung.

Ziel dieser Aufgabe ist es eine einfache Vorlage (Default) zu verwenden, um im weiteren Verlauf die Visualisierung

selbst zu gestalten.

1)In der Logical View den Root-Knoten auswählen

2)Aus dem Objektkatalog das Element "mapp View" auswählen und mittels Drag/Drop oder Doppelklick den Aus-

wahldialog öffnen

3)Die Vorlage "Default" auswählen und den Dialog schließen

## Page 14

14ARBEITEN MIT MAPP VIEW TM611

Ergebnis:

Eine mapp View Visualisierung besteht aus Elementen in der Logical View und in der Configuration View.

In der Logical View wurde das mapp View Package mit einer Bildschirmseite (Page), in der Configuration

View die Visualisierung (.vis) eingefügt.

Abbildung 11: Projektstruktur nach dem Einfügen einer mapp View Vorlage

## Page 15

MAPP VIEW VISUALISIERUNGSVORLAGEN15

4.1.vis-Datei

Die mapp View Visualisierung (.vis) definiert, welche Visualisierungs-Komponenten der Logical View und Configuration

View für die Darstellung der Bildschirmseiten auf einem Client zusammengehören. Jede Konfiguration kann 1-n Visua-

lisierungen (.vis) enthalten.

Abbildung 12: Visualization ID und autoUpdate sind konfiguriert

Es wird empfohlen bei Nichtnutzung die .vis-Datei zu schließen, damit beim Arbeiten an der Visualisierung

die Referenzierung im Hintergrund eingetragen werden können.

Visualization ID

Das Element "" bestimmt, wie die Visualisierung vom Visualisierungs-Client aufgerufen werden kann.Visualization id

autoUpdate

Das Attribut "" in der Visualisierung definiert, ob Elemente der Visualisierung automatisch in die .vis-DateiautoUpdate

eingetragen werden (default=true) oder nicht (=false).

Es muss sichergestellt sein, dass das Attribut "autoUpdate" auf true gesetzt ist. Dadurch werden alle in der Logical-

oder Configuration View eingefügten Visualisierungs-Elemente automatisch in die Visualisierung aufgenommen.

Die erste, in der Logical View eingefügte Page, wird außerdem als "" verwendet.StartPage

Visualization \ mapp View \ Engineering \ Organization of the HMI application \ mapp View visualization

object \ Updating element IDs automatically

Aufgabe: Name der Visualisierung ändern

In mapp View werden alle Komponenten einer Visualisierung mit einer Id oder einem Namen versehen.

Generell sollte man sich für sein Projekt - wie das auch für Prozessvariablen der Fall ist - Namensregeln für die verwal-

teten Objekte überlegen.

Jede Datei eines bestimmten Typs (z. B. Content oder Page) innerhalb eines mapp View Projektes muss einen eindeu-

tigen Namen haben. Die Id wird dabei mit dem Dateinamen bzw. Packagenamen z. B. einer Page synchronisiert

1)Öffnen der Visualisierung

2)Die ID (= Name der Visualisierung) zu "Training" umbenennen

## Page 16

16ARBEITEN MIT MAPP VIEW TM611

Ergebnis:

Eine Änderung der ID synchronisiert auch den Dateinamen auf "Training.vis".

Abbildung 13: Synchronisierung des Dateinamens mit der ID

4.2Visualisierung im Browser

Die Visualisierung kann mit einem Browser ab dem Zeitpunkt getestet werden, ab welcher in der Visualisierung (.vis)

die ersten Visualisierungsseiten (.page) referenziert wurden.

Durch das Einfügen des Vorlageprojekts ist bereits eine Page (page_0.page) im Projekt vorhanden.

Nach einem erfolgreichem Build und Transfer auf die ARsim erfolgt die Auslieferung der Visualisierung an den Browser

(Google Chrome oder Microsoft Edge) mit der URL:

- sofern nur eine Visualisierung im Projekt vorhanden istlocalhost:81/index.html

•

- Angabe der Visualisierungs ID sofern mehrere Visualisierungen imlocalhost:81/index.html?visuId=Training

•

Projekt vorhanden sind

Alternativ kann auch eine Default Visualisierung in der mapp View Konfiguration im Bereich "Client Konfiguration"

eingetragen werden.

Visualization \ mapp View \ Guides \ Getting started \ Testing the HMI application in the browser

Aufgabe: Aufruf der Visualisierung im Browser

Nach dem Einfügen der Visualisierungsvorlage ist bereits die erste Page vorhanden und in der Visualisierung (.vis)

automatisch referenziert. Das Projekt kann kompiliert und auf die ARsim übertragen werden.

1)Kompilieren und Übertragen des Projektes auf die ARsim

2)Aufruf der Visualisierung im Browser

## Page 17

MAPP VIEW VISUALISIERUNGSVORLAGEN17

Ergebnis:

Die Visualisierung kann anschließend unter folgender URL im Browser angezeigt werden:

http://127.0.0.1:81/index.html

Abbildung 14: Darstellung der Visualisierung "Training" im Browser

## Page 18

18ARBEITEN MIT MAPP VIEW TM611

5Seitenerstellung

Mit dem Einfügen einer Visualisierungsvorlage aus dem Objektkatalog wird ein  in die Logical Viewmapp View Package

eingefügt.

Unterhalb des mapp View Packages sind weitere Packages enthalten, in welche definierte mapp View Elemente ver-

waltet werden können.

Abbildung 15: mapp View- und Visualisierungs Package in der Logical View

Des Weiteren ist auch ein Visualisierungs Package eingefügt, welches die für die Visualisierung notwendigen Elemente

wie Pages, Contents usw. verwaltet.

In mapp View werden alle Komponenten einer Visualisierung mit einer ID oder einem Namen versehen. Jede Datei

muss innerhalb eines Packages einen eindeutigen Namen haben (entspricht auch der ID), welche über gleichartige

Komponenten eindeutig sein muss. So darf es in der Logical View keine zwei Contents mit dem gleichen Namen geben.

Visualization \ mapp View \ Engineering \ Organization of the HMI application \ Naming mapp View

components

## Page 19

SEITENERSTELLUNG19

5.1Layout

Ein Layout strukturiert einen großen Bereich in mehrere kleinere

Teilbereiche. Der vom Layout strukturierte Bereich wird durch die

Parameter "width" und "height" in Pixel definiert. Zur eindeutigen

Identifizierung hat ein Layout eine ID.

Die Abbildung Layout mit einer Breite und Höhe in Pixel zeigt den

Grundriss eines Layouts.

Abbildung 16: Layout mit einer Breite und Höhe in Pixel

Visualization \ mapp View \ Engineering \ Layout and areas

Wenn möglich, sollten für die am häufigsten verwendeten Seiten der Visualisierung dasselbe Layout ver-

wendet werden. Denn eine Änderung des Layouts während eines Seitenwechsels löst einen vollständigen

Neuaufbau der Seite aus.

Aufgabe: Layout bearbeiten

Durch die mapp View Vorlage wurde bereits ein Layout mit eingefügt. Ziel dieser Aufgabe ist es, dieses Layout ent-

sprechend zu bearbeiten.

1)In der Logical View unter "mapp View / Visualization" auf "Layouts" klicken

2)Die Layout-Datei "layout_0" auf "MyLayout" umbenennen

3)Die Layout-Datei öffnen und die Größe des Layouts ändern:

Common \ NameLayout \ widthLayout \ height

MyLayout1280800

Ergebnis:

Das Layout hat eine bestimmte Größe erhalten.

Abbildung 17: In der Logical View ist "MyLayout.layout" eingefügt und grafisch bearbeitet

## Page 20

20ARBEITEN MIT MAPP VIEW TM611

5.2Area

Areas definieren die Teilbereiche eines Layouts. Jede Area hat in-

nerhalb eines Layouts eine eindeutige ID. Eine Area wird bestimmt

durch seine Größe ("width" und "height") und durch seine Position

("top" und "left") innerhalb des Layouts, welche jeweils in Pixel an-

gegeben werden.

Der Bezugspunkt für die Position von Areas innerhalb eines Lay-

outs ist die linke obere Ecke.

Eine Area mit dem Namen (ID) "area1" ist nach dem Einfügen des

Layouts bereits vorhanden und kann wiederverwendet werden.

Eine neue Area wird über das entsprechende Icon in der Werk-Abbildung 18: Layout mit drei Areas

zeugleiste oder durch Doppelklick auf eine leere Fläche des Lay-

outs eingefügt.

Visualization \ mapp View \ Engineering \ Layout and areas \ Layout Editor \ Configuring areas

Aufgabe: Areas bearbeiten

Ziel dieser Aufgabe ist das Aufteilen des Layouts "MyLayout.layout" in drei Bereiche / Areas.

Mit der Visualisierungsvorlage wird ein Layout mit einer Area geliefert. Diese Area ("area1") ist mit dem vorhandenen

Content ("content_0") verknüpft.

1)Die Eigenschaften von "area1" auf "AreaMain" ändern und Position bzw. Größe anpassen

Common \ NameLayout \ topLayout \ leftLayout \ widthLayout \ height

AreaMain1001501130700

2)Mit einem Doppelklick auf eine leere Position im Layout zwei weitere Areas anlegen

3)Die Eigenschaften der beiden neuen Areas ändern:

Common \ NameLayout \ topLayout \ leftLayout \ widthLayout \ height

AreaTop001280100

AreaLeft1000150700

Die Größe des Layouts und der anderen grafischen mapp View Editoren kann mit <STRG> + Mausrad

vergrößert oder verkleinert werden.

## Page 21

SEITENERSTELLUNG21

Ergebnis:

Das fertige Layout besteht aus drei Areas: "AreaTop", "AreaLeft" und "AreaMain".

Abbildung 19: "MyLayout.layout" mit drei Areas

## Page 22

22ARBEITEN MIT MAPP VIEW TM611

5.3Page

Eine Page (Visualisierungsseite) definiert jenen Inhalt, der auf dem sichtbaren Bereich eines Visualisierungs-Clients

dargestellt werden kann.

Die Page wird durch einen global eindeutigen Namen (ID) identifiziert. Die Page definiert, welches Layout für die Struk-

turierung in die Teilbereiche verwendet wird und mit welchen Inhalten (Content) die einzelnen Teilbereiche versehen

werden. Den Areas können Hintergrundfarben zugewiesen werden.

Visualization \ mapp View \ Engineering \ Pages \ Adding pages

Aufgabe: Page bearbeiten

Durch die Visualisierungsvorlage wurde bereits eine Page (page_0.page) mit eingefügt. Ziel dieser Aufgabe ist es eine

Visualisierungsseite namens "MainPage" anzulegen. Der Seite soll das Layout "MyLayout" zugewiesen werden. Zur bes-

seren Unterscheidbarkeit bekommen die drei Teilbereiche (Areas) unterschiedliche Hintergrundfarben. Als Farbcode

wird ein Hexadezimalcode bei der Eigenschaft "backColor" angegeben. Im Anschluss werden die Contents erstellt.

1)In der Logical View unter "mapp View / Visualization" auf "Pages" klicken

2)Die Page-Datei "page_0.page" öffnen

3)Die Eigenschaft PageId von "page_0" auf "MainPage" ändern / umbenennen

Auf der Page ist an der Eigenschaft "LayoutId" bereits das zuvor bearbeitete Layout "MyLayout" referenziert. Da-

durch erhält die Page auch die darin verwendete Strukturierung der Areas.

4)Auf die Areas klicken und deren Eigenschaften ändern:

Appearance \ backColor

AreaToprgba(227, 227, 227, 1)

AreaLeftrgba(227, 227, 227, 1)

AreaMainrgba(245, 245, 245, 1)

Ergebnis:

Nach dem Speichern der Page wird der Name der Page ("pageId") mit dem Page Package und der .pa-

ge-Datei synchronisiert.

Die Areas werden mit den entsprechenden Hintergrundfarben angezeigt.

Abbildung 20: Ergebnis der Visualisierungsseite "MainPage"

## Page 23

SEITENERSTELLUNG23

5.4Content & Widgets

Ein Content stellt einen in einer Visualisierung darstellbaren Inhalt dar. Ein Content wird identifiziert durch eine global

eindeutige ID sowie durch seine Größe ("width" und "height").

In einem Content können Widgets platziert und projektiert werden.

Die Abbildung zeigt einen Content mit folgenden

Widgets:

Button

•

Label

•

NumericOutput

•

NumericInput

•

Image

•

Widgets können immer über den oderObjektkatalog

über das (Rechtsklick) eingefügt wer-Kontextmenü

den.

Ein Content, welcher auf mehreren Pages wiederverwendet wird, kann im Package "AreaContents" ein-

gefügt und verwaltet werden.

Visualization \ mapp View \ Engineering \ Piece of content on a page \ Adding a piece of content

Aufgabe: Content umbenennen

In dieser Aufgabe wird der aus Visualsierungsvorlage vorhandene "content_0" auf den Namen "ContentMainPage" um-

benannt und dessen Größe angepasst. Aufgrund der Visualisierungsvorlage ist die AreaMain schon mit dem Content

verknüpft und dieser kann einfach geöffnet werden.

Ein Content kann auch in der Logical View geöffnet und bearbeitet werden.

1)"MainPage.page" öffnen und "AreaMain" auswählen

Diese Area ist bereits mit dem vorhandenem Content aus der Visualisierungsvorlage verlinkt.

2)Über Rechtsklick "Content öffnen" oder über einen Doppelklick den verlinkten Content öffnen

3)In den Eigenschaften des Contents den Namen und die Größe anpassen

## Page 24

24ARBEITEN MIT MAPP VIEW TM611

Common \ NameProperty \ heightProperty \ width

ContentMainPage7001130

Ergebnis:

Nach dem Speichern des Contents und der Page wird der Content Name angepasst.

Abbildung 21: Aus "content_0" wird "ContentMainPage"

Aufgabe: Contents erstellen

In dieser Aufgabe werden im Page Editor der "MainPage" an der "AreaTop" und "AreaLeft" die entsprechenden Contents

erzeugt.

Ein Content kann auch in der Logical View aus dem Objektkatalog eingefügt und bearbeitet werden.

1)In "MainPage.page" die "AreaTop" auswählen

2)Über das Kontextmenü "Content erstellen" oder einen Doppelklick auf die unreferenzierte Area einen neuen Con-

tent mit der entsprechenden Größe und Referenz zur Area erstellen

3)Im geöffneten Dialog auf der entsprechenden Area den Namen des Contents projektieren

AreaContent - Name

AreaTopContentTop

AreaLeftContentLeft

4)"MainPage" speichern, damit die Content Referenzen gesichert sind

## Page 25

SEITENERSTELLUNG25

Ergebnis:

Im Paket "MainPage" werden zwei neue Contents erzeugt.

Abbildung 22: "ContentLeft" und "ContentTop" im Paket "MainPage"

Aufgabe: Allgemein verwendete Contents in Package AreaContents verschieben

In dieser Aufgabe werden die Contents "ContentTop" und "ContentLeft" in das Package "AreaContents" verschoben,

da diese auf jeder weiteren Page der Visualisierung wiederverwendet werden. Dies dient lediglich der einfacheren Ver-

waltung dieser Contents.

1)Im Paket "MainPage" "ContentTop" und "ContentLeft" auswählen (Mehrfachslektion mit <STRG>)

2)Mit Drag&Drop die Contents in das Package "AreaContents" verschieben

Ergebnis:

Die beiden Contents können für weitere Aufgaben im Package "AreaContents" bearbeitet werden.

Abbildung 23: "ContentTop" und "ContentLeft" im Paket "AreaContents"

Aufgabe: Aufruf der Visualisierung im Browser

Ziel dieser Aufgabe ist die Darstellung der "MainPage" im Browser.

1)Kompilieren und Übertragen des Projektes auf die ARsim

2)Aufruf der Visualisierung im Browser

## Page 26

26ARBEITEN MIT MAPP VIEW TM611

Ergebnis:

Die "MainPage" wird durch die Farben der Areas im Browser entsprechend dargestellt.

http://127.0.0.1:81/index.html

Abbildung 24: Darstellung der Visualisierung "Training" im Browser

## Page 27

NAVIGATION27

6Navigation

Eine Visualisierung besteht in der Regel aus mehr als einer Page. Um zwischen diesen Visualisierungsseiten wechseln

zu können, wird eine Navigation benötigt.

Die Navigation einer Visualisierung bestimmt, wie zwischen den Pages navigiert werden kann.

Die nebenstehende Abbildung zeigt die drei Pages "MainPage",

"ServicePage" und "InfoPage" und die Möglichkeiten der Navi-

gation zwischen diesen.

Konkret kann in diesem Beispiel von jeder Page auf jede ande-

re Page navigiert werden.

Um die Navigation für eine Visualisierung zu definieren, stehen

zur Verfügung - die Navigationzwei Möglichkeitenmanuelle

und die Navigation. Beide Arten der Navigationautomatische

können kombiniert werden.

Abbildung 25: Navigationsschema

6.1Manuelle Navigation

Die manuelle Navigation wird mittels einzelner NavigationButton-Widgets realisiert. Für jede Page auf die von der je-

weils aktuellen Page aus navigiert werden kann, muss ein NavigationButton-Widget platziert und konfiguriert werden.

Die nebenstehende Abbildung zeigt, dass auf jeder

Page drei NavigationButton-Widgets platziert und

konfiguriert sind. Jeder einzelne NavigationButton

ist mit der ID der Page konfiguriert, auf die navigiert

werden soll.

Visualization \ mapp View \ Widgets \

Container \ NavigationBar

Abbildung 26: Navigationsschema manuelle Navigation

Die manuelle Navigation ist gut geeignet, wenn für alle Pages die gleichen NavigationButton-Widgets verwendet wer-

den können. In diesem Fall müssen diese in einem Content positioniert und konfiguriert werden. Dieser Navigation

Content kann dann für jede Page wiederverwendet (referenziert) werden.

Ein Nachteil der manuellen Navigation ist, dass sie nicht kontextabhängig ist. Die Anordnung und Konfiguration der

NavigationButton-Widgets ist auf jeder Page gleich. Soll Anordnung und Konfiguration der NavigationButton-Widgets

abhängig von der jeweiligen Page unterschiedlich sein, müssen verschiedene Contents für die Navigation erstellt wer-

den und bei der entsprechenden Page individuell referenziert werden.

6.2Automatische Navigation

Die automatische Navigation basiert auf der Definition einer Navigations-Struktur. Mit dieser werden Navigationspfa-

de festgelegt, also von welcher Page auf welche anderen Pages navigiert werden kann. mapp View kümmert sich zur

Laufzeit um die Platzierung der entsprechenden NavigationButton-Widgets auf jeder Page.

Auf jeder Page sind nur jene Navigations-Ziele (andere Pages) sichtbar, die in der Navigations-Struktur als Navigati-

onspfad für die jeweilige Page definiert sind. Die Anzeige der NavigationButton-Widgets ist kontextsensitiv.

## Page 28

28ARBEITEN MIT MAPP VIEW TM611

Die nebenstehende Abbildung zeigt eine auto-

matische Navigation für drei Pages: "MainPage",

"ServicePage" und "InfoPage". Man kann von jeder

Page auf jede andere Page navigieren, aber jede Pa-

ge zeigt nur die kontextspezifischen Navigationszie-

le an.

Visualization \ mapp View \ Enginee-

ring \ Organization of the HMI applica-

tion \ mapp View file reference (XML) \

Automatic navigation (.navigation)

Abbildung 27: Navigationspfade automatische Navigation

6.3Manuelle Navigation anwenden

Die Manuelle Navigation benötigt weitere Pages, zwischen denen mit Hilfe der NavigationButton-Widgets navigiert

werden kann.

Dabei kann bei der Erstellung einer neuen Page die bestehende  dupliziert bzw. eine neue Page aus dem"MainPage"

Objektkatalog eingefügt und projektiert werden.

Beim Kopieren und Einfügen eines Page-Packages wird der Name der Page (ID) und die im Package vor-

handenen .content-Dateien mit einem neuen Namen mit aufsteigender Nummer erstellt.

Der Vorteil beim Kopieren einer Page besteht darin, dass die Hintergrundfarben und Content-Referenzen

der Areas übernommen werden.

Aufgabe: "ServicePage" und "InfoPage" erstellen

In dieser Aufgabe werden zwei weitere Pages mit jeweils einem neuen Content ("ContentServicePage" und

"ContentInfoPage") erstellt.

Die Pages verwenden das Layout "MyLayout" und referenzieren in AreaMain auf "ContentServicePage" und

"ContentInfoPage".

Eine neue Page aus dem Objektkatalog unter das Pages Package eingefügt oder durch kopieren der

"MainPage" erstellt werden.

Die Kopie erhält einen neuen Namen ("MainPage" => "MainPage1") und kann durch Umbenennen entspre-

chend angepasst werden. Enthält ein Page Package beim Kopieren auch einen oder mehrere Content(s),

erhalten auch diese in der Kopie einen neuen Namen und sind auch in der Page entsprechend referenziert.

1)"ServicePage" erstellen (entweder aus dem Objektkatalog oder als Kopie der "MainPage")

Common \ refId

AreaMainContentServicePage

2)"InfoPage" erstellen (entweder aus dem Objektkatalog oder als Kopie der "MainPage")

Common \ refId

AreaMainContenInfoPage

## Page 29

NAVIGATION29

Ergebnis:

In der Logical View sind die neuen Pages "ServicePage" und "InfoPage" enthalten. Beide Pages referen-

zieren an "AreaTop" - " ContentTop" und an "AreaLeft" - "ContentLeft".

An "AreaMain" wird in der "ServicePage" - "ContentServicePage" und in der "InfoPage" - "ContentInfoPage"

referenziert.

Abbildung 28: Neue Pages in der Logical View erstellt

6.4NavigationBar-Widget

Das NavigationBar-Widget dient als Container für NavigationButton-Widgets.

Werden NavigationButtons in die NavigationBar eingefügt, können diese automatisch positioniert bzw. anhand der

bereits eingefügten NavigationButtons ausgerichtet werden. Dies kann über die Eigenschaft "childPositioning" einge-

stellt werden.

Ist die Option "relativ" ausgewählt, werden die Child-Widgets automatisch der Reihe nach positioniert. Der Anwender

erspart sich dadurch die manuelle Ausrichtung der einzelnen NavigationButton-Widgets.

Visualization \ mapp View \ Widgets \ Container \ NavigationBar

Aufgabe: NavigationBar-Widget einfügen

In dieser Aufgabe wird auf "ContentLeft" das NavigationBar-Widget eingefügt und parametriert.

1)"ContentLeft" öffnen

2)Aus dem Objektkatalog (oder über das Kontextmenü) das NavigationBar-Widget einfügen

3)Die Eigenschaften des NavigationBar-Widgets ändern:

Behavior \ childPositioningLayout \ PositionLayout \ Size

relative0; 0150; 300

## Page 30

30ARBEITEN MIT MAPP VIEW TM611

Damit die Eigenschaften eines Widgets übersichtlicher dargestellt werden, können die stylebaren Ei-

genschaften ausgeblendet werden. Diese werden erst zu einem späteren Zeitpunkt wieder benötigt.

Abbildung 29: Stylebare Eigenschaften ausblenden

Ergebnis:

Die Eigenschaften "childPositioning", "Position" und "Size" des NavigationBar-Widgets sind angepasst.

Abbildung 30: Angepasste Eigenschaften des NavigationBar-Widgets

6.5NavigationButton-Widget

Das NavigationButton-Widget dient zur Navigation zwischen den unterschiedlichen Pages.

Der Text, der auf dem NavigationButton dargestellt wird, wird an der Eigenschaft "Appearance \ text" eingegeben.

Der eindeutige Name des NavigationButtons wird an der Eigenschaft "Common \ Name" definiert.

Die Page, auf die beim Betätigen des NavigationButtons navigiert werden soll, wird an der Eigenschaft "Data \ pageId"

ausgewählt.

Visualization \ mapp View \ Widgets \ Buttons \ NavigationButton

Aufgabe: NavigationButton-Widgets einfügen

In dieser Aufgabe werden im NavigationBar-Widget die NavigationButton-Widgets eingefügt und parametriert.

1)Auf "ContentLeft" das NavigationBar-Wigdet markieren

2)Aus dem Objektkatalog (oder über das Kontextmenü) drei NavigationButton-Widgets einfügen

3)Die Eigenschaften der NavigationButton-Widgets ändern:

Appearance \ textCommon \ NameData \ pageIdLayout \ marginLayout \ Size

MainPageMainPageButtonMainPage5px140;60

ServicePageServicePageButtonServicePage5px140;60

## Page 31

NAVIGATION31

Appearance \ textCommon \ NameData \ pageIdLayout \ marginLayout \ Size

InfoPageInfoPageButtonInfoPage5px140;60

Ergebnis:

Die Eigenschaften "text", "Name", "pageId", "margin" und "Size" der drei NavigationButton-Widgets sind

angepasst.

Abbildung 31: Angepasste Eigenschaften des NavigationButton-Widgets von "MainPageButton"

Aufgabe: Visualisierung im Browser öffnen

Nachdem alle NavigationButton-Widgets eingefügt und konfiguriert wurden, kann das Projekt kompiliert und auf die

ARsim übertragen werden.

Ergebnis:

Die Visualisierung kann anschließend unter folgender URL angezeigt werden:

http://127.0.0.1:81

Abbildung 32: Visualisierung im Browser mit manueller Navigation

## Page 32

32ARBEITEN MIT MAPP VIEW TM611

7Optische Gestaltung - Styling

Styling bezeichnet die Möglichkeit für die Gestaltung des Erscheinungsbildes einer Visualisierung unabhängig von

deren Funktionalität.

Die Gestaltung des Erscheinungsbildes einer Visualisierung sowie einzelner Elemente einer Visualisierung erfolgt über

styleable Properties, Styles und über Themes.Die Navigation einer Visualisierung bestimmt, wie zwischen den Pages

navigiert werden kann.

Visualization \ mapp View \ Engineering \ Themes and styles

7.1Styleable Property

Eine styleable Property (stylebare Eigenschaft) be-

schreibt eine einzelne Eigenschaft eines Visualisierungs-

elementes, die das Erscheinungsbild beeinflusst.

Jeder Widgettyp hat festgelegte stylebare Eigenschaften,

die in der Automation Help beim jeweiligen Widget be-

schrieben sind. Ein Beispiel für eine stylebare Eigenschaft

am Button-Widget ist backColor.

Abbildung 33: Navigationsschema

Wird ein Widget öfter verwendet, muss für jede einzelne Widget Instanz die stylebaren Eigenschaften geändert wer-

den. Diese Vorgehensweise bedeutet viel Aufwand und die Fehleranfälligkeit ist unverhältnismäßig hoch. Um das Er-

scheinungsbild einer größeren Anzahl von Widget Instanzen zu verändern, bietet die Verwendung von Styles die bes-

sere Möglichkeit.

Aufgabe: Stylable Properties der NavigationButton-Widgets ändern

Ziel dieser Aufgabe ist es, die Hintergrundfarbe der NavigationButton-Widgets über die stylebaren Eigenschaften zu

ändern.

1)Auf "ContentLeft" die NavigationButton-Widgets markieren

2)Die Eigenschaften der NavigationButton-Widgets ändern

Appearance \ backColor

MainPageButtonrgba(255, 255, 192, 1)

ServicePageButtonrgba(192, 255, 192, 1)

InfoPageButtonrgba(192, 255, 192, 1)

## Page 33

OPTISCHE GESTALTUNG - STYLING33

Ergebnis:

Die Seite "ServicePage" wird dargestellt. Da die Eigenschaft "backColor" nur für den "nicht-gedrück-

ten-Zustand" angewendet wird, zeigt der aktive NavigationButton die Default-Farbe für den gedrückten

Zustand an.

Abbildung 34: Optische Gestaltung der Navigation durch styleable properties

7.2Style

Ein Style fasst für einen Typ eines Visualisierungselements die Werte aller seiner stylebaren Eigenschaften zusammen.

Für jeden Typ eines Visualisierungselementes kann es mehrere Styles geben.

Der Style eines Button-Widgets (Button-Style) beinhaltet für jede stylebare Eigenschaft des Widgets einen konkreten

Wert. Für das Button-Widget kann es mehrere Styles geben, z. B. Command, Operate, Default.

7.3Theme

Um einer Visualisierung schnell und einfach ein ansprechendes Erscheinungsbild zu geben, werden von B&R Themes

zur Verfügung gestellt. Darin sind für verschiedene Typen von Visualisierungselementen Styles vordefiniert, die mit

ihrem Style-Namen angewendet werden können.

Ein typischer Anwendungsfall für Themes ist die Tag-Nacht-Umschaltung bei Visualisierungen in denen das natürliche

Umgebungslicht eine Rolle spielt. So haben im Theme für die Nacht die Widgets dunkle Hintergrundfarben und helle

Schriftfarben, während die Styles für den Tag helle Hintergrundfarben und dunkle Schriftfarben konfiguriert haben.

Abbildung 35: Tag: light ThemeAbbildung 36: Nacht: dark Theme

## Page 34

34ARBEITEN MIT MAPP VIEW TM611

Visualization \ mapp View \ Engineering \ Themes and styles \ Adding a theme

Aufgabe: B&R Theme einfügen und Style auf Button-Widgets anwenden

Ziel dieser Aufgabe ist es, das mitgelieferte Theme Package einzufügen. Die darin vorhandenen Styles werden auf

zwei neu erstellte Button-Widgets angewendet. Die stylebaren Eigenschaften der NavigationButton-Widgets werden

zurückgesetzt und stattdessen der Default-Style geladen.

1)In der Logical View unter "mapp View \ Resources" auf "Themes" klicken

2)Aus dem Objektkatalog das Paket "BuRThemeFlatLight Package" einfügen

3)"ContentMainPage" öffnen

4)Aus dem Objektkatalog zwei Button-Widgets nebeneinander einfügen

5)In der Werkzeugleiste auf das Icon "Editor Theme" klicken und "BuRThemeFlatLight" auswählen

6)Die Eigenschaften des linken Buttons ändern: "Appearance \ Style \ Default" = "Command1",

"Size" = 100; 60

7)Die Eigenschaften des rechten Buttons ändern: "Appearance \ Style \ Default" = "Operate1",

"Size" = 100; 60

8)"ContentLeft" öffnen und "backColor" der drei NavigationButtons mit einem Rechtsklick und "Reset" zurückset-

zen.

Ergebnis:

Das Theme ist Teil des Projekts und auf zwei Button-Widgets sind die Styles "Command1" und "Operate1"

angewendet.

Abbildung 37: Theme Package ist eingefügt, das Theme ist auf die Visualisierung angewendet und zwei Button-Widgets haben einen

Style

## Page 35

OPTISCHE GESTALTUNG - STYLING35

Der Content Editor wird standardmäßig mit einem weißen Hintergrund dargestellt. Werden Widgets mit

überwiegend weißem Anteil eingefügt, kann über die Designer Einstellungen ein Editor-Hintergrund ein-

gestellt werden. Dieser wird über die Toolbar des Content Editors geöffnet.

Abbildung 38: Hintergrundfarbe im Content Editor anpassen

## Page 36

36 ARBEITEN MIT MAPP VIEW TM611
8 Datenanbindung
8.1 Binding
In einer Visualisierung werden Daten angezeigt und von Benutzern eingegeben. In mapp View wird diese Verbindung
über das Konzept des Bindings (Data-Binding) realisiert.
Ein Binding ist eine konfigurationsspezifische Defintion, welche die Verbindung zwischen einer Source (z. B. OPC-UA-
Variable) und einem Target (z. B. Widget Eigenschaft) und dessen Lese-/Schreibrichtung (Binding Mode) festlegt.
In mapp View gibt es verschiedene Arten von Bindings für verschiedene Zwecke:
"Werte Binding" bzw. "Value Binding" für das Anbinden einfacher Werte
•
"Node Binding" für das Anbinden von OPC UA Nodes mit Einheit und Limits
•
"Array Binding" für das Anbinden von Feldern (Arrays)
•
"Listen Binding" für die Auswahl von Variablen aus einer Liste
•
"Struktur-Listen Binding" für die Auswahl von Struktur-Variablen aus einer Liste
•
"Komplexes Binding" für das Anbinden von Strukturen
•
In diesem Trainingsmodul werden nur die Binding Arten "Werte Binding" und "Node Binding" behandelt.
Binding Modes
Durch die Angabe des Binding Modes wird die Richtung definiert, in die der Datenfluss erfolgen soll.
Binding Mode "oneWay" (Read Only)
Der Binding Mode "oneWay" wird für lesenden Zugriff auf eine Quelle verwendet.
Beispiel: Binding zwischen OPC UA Node und einem Ausgabe-Widget (z. B. NumericOutput-Widget).
Binding Mode "twoWay" (Read / Write)
Der Binding Mode "twoWay" wird für lesenden und schreibenden Zugriff auf die Quelle verwendet.
Beispiel: Binding zwischen OPC UA Node und einem Eingabe-Widget (z. B. NumericInput-Widget).
Binding Mode "oneWayToSource" (Init Read / Write)
Der Binding Mode "oneWayToSource" wird nur für schreibenden Zugriff auf die Quelle verwendet.
Beispiel: Binding zwischen OPC UA Node und einem PushButton-Widget.
Visualization \ mapp View \ Engineering \ Variables and data \ Binding

## Page 37

DATENANBINDUNG37

8.2OPC UA

OPC Unified Architecture (OPC UA) ist ein Standard für Interoperabilität und ermöglicht den sicheren und zuverlässi-

gen Austausch von Daten in der industriellen Automatisierung. OPC UA ist plattformunabhängig und sorgt für den

nahtlosen Informationsfluss zwischen den Geräten verschiedener Hersteller.

Abbildung 39: OPC-UA-Architektur

OPC UA Node

Wird eine Prozessvariable über das OPC-UA-System verfügbar gemacht, kann sie von OPC-UA-Clients als OPC UA Node

gelesen oder beschrieben werden. Ein OPC UA Node kann um Properties, wie Engineering Unit oder EU Range, erweitert

werden.

Engineering Unit

Die Engineering Unit zu einem OPC UA Node gibt an, in welcher physikalischen Einheit der Wert zu interpretieren ist.

Automation Studio stellt 1400 Engineering Units für die sieben physikalischen Basisgrößen Länge, Masse, Zeit, Strom-

stärke, Temperatur, Stoffmenge und Lichtstärke sowie abgeleitete physikalische Größen (z. B. Geschwindigkeit, Kraft,

Druck, Beschleunigung) zur Auswahl bereit.

EU Range

Der EU Range (Engineering Unit Range) bezeichnet für einen OPC UA Node dessen gültigen Wertebereich. Der EU Range

wird durch einen unteren und einen oberen Grenzwert definiert.

OPC UA Default View

Die OPC UA Default View enthält alle Prozessvariablen einer Automatisierungsapplikation, die vom OPC-UA-Server auf

der B&R-Steuerung den OPC-UA-Clients zur Verfügung gestellt werden.

Communication \ OPC UA C/S \ Configuration in AS \ OPC UA C/S configuration

Communication \ OPC UA C/S \ Configuration in AS \ OPC UA C/S default view configuration

Communication \ OPC UA C/S \ Configuration in AS \ OPC UA C/S default view

## Page 38

38ARBEITEN MIT MAPP VIEW TM611

8.3Datenanbindung anwenden

Im Kapitel 3 "mapp View Authentifizierung" auf Seite 10 wurde der OPC-UA-Server bereits aktiviert und für einen an-

onymen Zugang über das HTTP Protokoll konfiguriert.

Damit ein OPC-UA-Client (mapp View Server) Prozessvariablen der Steuerung lesen oder schreiben kann, müssen diese

für in der OPC UA Default View "aktiviert" werden.

Mehrteilige Aufgabe - Datenanbindung

1)"Aufgabe: OPC-UA-Konfiguration - Variablen aktivieren" auf Seite 38

2)"Aufgabe: Label-, NumericInput- und NumericOutput-Widget einfügen" auf Seite 38

3)"Aufgabe: Value Binding zwischen NumericInput- und NumericOutput-Widget und globaler OPC-UA-Variable" auf

Seite 40

4)"Aufgabe: Node Binding zwischen NumericInput- und NumericOutput-Widget und globaler OPC-UA-Variable" auf

Seite 41

5)"Aufgabe: Binding Arten im Browser testen" auf Seite 42

Aufgabe: OPC-UA-Konfiguration - Variablen aktivieren

Ziel dieser Aufgabe ist es, die OPC-UA-Konfiguration so einzustellen, dass der Tag der globalen Variablen

"CurrentTemperature" und "SetTemperature" aktiviert ist. Der OPC UA Node wird dadurch aktiviert.

Damit die Kommunikation zwischen Automatisierungsapplikation und der Visualisierung erfolgen kann, muss der

OPC-UA-Server aktiviert werden. Um die Prozessvariable in der Visualisierung verwenden zu können, muss diese als

OPC UA Node deklariert werden.

1)In der Configuration View unter "Connectivity" auf "OpcUACs" klicken

2)Aus dem Objektkatalog das Objekt "Default View" einfügen und die Datei "OpcUaCsMap.uad" öffnen

3)Rechtsklick auf die globalen Variablen "CurrentTemperature" und "SetTemperature" und "Enable Tag" klicken

Ergebnis:

Der OPC UA Node der Variablen "CurrentTemperature" und "SetTemperature" ist aktiviert. Die Schrift der

Einträge ist nicht mehr ausgegraut.

Abbildung 40: Gloable Prozessvariablen in OPC UA Default View aktivieren

Aufgabe: Label-, NumericInput- und NumericOutput-Widget einfügen

In dieser Aufgabe werden zwei Label-Widgets eingefügt, um die verschiedenen Binding Arten auf der Visualisierungs-

seite zu beschriften.

Weiters werden für jede Binding Art je ein NumericInput-Widget für die Eingabe der Solltemperatur und je ein Nume-

ricOutput-Widget für die Anzeige der Isttemperatur benötigt. Die Beschriftung für Solltemperatur und Isttemperatur

erfolgt ebenfalls über Label-Widgets.

## Page 39

DATENANBINDUNG 39
1) "ContentMainPage" öffnen
2) Aus dem Objektkatalog zwei Label-Widgets für die Beschreibung der Binding Art auf der linken Seite des Con-
tents platzieren
Common \ Name Appearance \ text
LabelValueBinding Value binding
LabelNodeBinding Node binding
3) Aus dem Objektkatalog zwei Label-Widgets für die Beschreibung der Variablen nebeneinander platzieren
Common \ Name Appearance \ text
LabelSetTemp Set temperature
LabelCurrentTemp Current temperature
4) Aus dem Objektkatalog zwei NumericInput-Widgets für die Eingabe der Solltemperatur für beide Binding Arten
platzieren
Common \ Name
NumericInputSetTempValue
NumericInputSetTempNode
5) Aus dem Objektkatalog zwei NumericOutput-Widgets für die Anzeige der Isttemperatur für beide Binding Arten
platzieren
Common \ Name
NumericOutputCurrentTempValue
NumericOutputCurrentTempNode
6) Projekt kompilieren, übertragen und in Visualisierung anzeigen
Die sinnvolle Bennennung der Widgets über die Eigenschaft "Common \ Name" ist von großer Bedeutung,
wenn später Event-Bindings erstellt werden sollen.

## Page 40

40ARBEITEN MIT MAPP VIEW TM611

Ergebnis:

Bis jetzt sieht man in der Visualisierung noch keine gültigen Werte, da noch keine OPC-UA-Variablen ge-

bunden sind und auch keinen Unterschied zwischen Node Binding und Value Binding. Nach der Einhei-

tenprojektierung im Kapitel 11.7 "Einheitensystem" auf Seite 60 wird man einen Unterschied sehen

können.

Abbildung 41: Visualisierung im Browser mit projektierten Widgets

Aufgabe: Value Binding zwischen NumericInput- und NumericOutput-Widget und globaler OPC-UA-Variable

In dieser Aufgabe werden die globalen OPC-UA-Variablen mit den entsprchenden Widgets verbunden.

"CurrentTemperature" wird mit NumericOutput-Widget (Name = NumericOutputCurrentTempValue) verbunden.

Binding Mode = Read Only

•

Binding Art = Value Binding

•

"SetTemperature" wird mit NumericInput-Widget (Name = NumericInputSetTempValue) verbunden.

Binding Mode = Read / Write

•

Binding Art = Value Binding

•

1)Aufdas NumericOutput-Widget mit dem Namen "NumericOutputCurrentTempValue" klicken

2)Im Eigenschaftsfenster unter "Data \ Value" auf das Feld "Binding" klicken und den Auswahldialog öffnen

3)Im Tab "OPC-UA" unter "Global Variables \ CurrentTemperature" auf "value" klicken

4)Unter Binding Mode "Read Only" auswählen

5)Unter "Bindings Set Id" die Checkbox "Content Related" aktivieren

6)Auswahldialog mit OK schließen

7)Den Vorgang mit dem NumericInput-Widget "NumericInputSetTempValue" für ein Binding der globalen OPC-UA-

Variable "SetTemperature" wiederholen - hier wird als Binding Mode "Read / Write" verwendet.

## Page 41

DATENANBINDUNG41

Ergebnis:

Das NumericOutput-Widget hat ein Value Binding zur Variable "CurrentTemperature".

Abbildung 42: NumericOutput-Widget zeigt "CurrentTemperature", Binding Art = Value Binding

Aufgabe: Node Binding zwischen NumericInput- und NumericOutput-Widget und globaler OPC-UA-Variable

In dieser Aufgabe wird die globale OPC-UA-Variable "CurrentTemperature" mit dem NumericOutput-Widget (Name =

NumericOutputCurrentTempNode) verbunden.

Binding Mode = Read Only

•

Binding Art = Node Binding

•

Die globale Variable "SetTemperature" wird mit dem NumericInput-Widget (Name = NumericInputSetTempNode) ver-

bunden.

Binding Mode = Read / Write

•

Binding Art = Node Binding

•

1)Aufdas NumericOutput-Widget mit dem Namen "NumericOutputCurrentTempNode" klicken

2)Im Eigenschaftsfenster unter "Data \ Value" auf das Feld "Binding" klicken und den Auswahldialog öffnen

3)Im Tab "OPC-UA" auf "Global Variables \ CurrentTemperature" klicken

4)Unter Binding Mode "Read Only" auswählen

5)Unter "Bindings Set Id" die Checkbox "Content Related" aktivieren

6)Auswahldialog mit OK schließen

7)Den Vorgang mit dem NumericInput-Widget "NumericInputSetTempNode" für ein Binding der globalen OPC-UA-

Variable "SetTemperature" wiederholen - hier wird als Binding Mode "Read / Write" verwendet.

## Page 42

42ARBEITEN MIT MAPP VIEW TM611

Ergebnis:

Das NumericOutput-Widget hat ein Node Binding zur Variable "CurrentTemperature".

Abbildung 43: NumericOutput-Widget zeigt "CurrentTemperature", Binding Art = Node Binding

Aufgabe: Binding Arten im Browser testen

Ziel ist das Eingabeverhalten zwischen Value Binding und Node Binding zu testen.

Bei den NumericOutput-Widgets ist der Unterschied zwischen Value- und Node Binding erst bei der Ver-

wendung des Einheitensystems ersichtlich.

## Page 43

DATENANBINDUNG43

1)Projekt kompilieren, übertragen und in Visualisierung anzeigen

a)Eingabefeld "Value binding" öffnen

Ergebnis:

Beim Value Binding wird nur der Wert der OPC-UA-Variable verwendet. Als Limits werden die Ei-

genschaften "minValue" und "maxValue" des NumericInput-Widgets verwendet.

Abbildung 44: Eingabe Limits bei Value Binding

b)Eingabefeld "Node binding" öffnen

Ergebnis:

Beim Node Binding werden zusätzlich zum Wert auch die Limts (EU Range) und die Einheit (Engi-

neering Unit) der OPC-UA-Variable verwendet. Da an der OPC-UA-Variable noch keine EU Range

projektiert wurde, werden die maximal möglichen Werte für das obere und untere Limit verwen-

det.

Abbildung 45: Eingabe Limits bei Node Binding

Bis jetzt kann die Solltemperatur ("SetTemperature") im Node Binding auf nahezu jeden beliebigen Wert gesetzt wer-

den. Das ist nicht immer sinnvoll. Um zu verhindern, dass der Nutzer nicht zulässige Werte eingibt, wird der EU Range

(Limits Low-/High-Werte) des OPC UA Nodes definiert.

Aufgabe: OPC-UA-Konfiguration - Wertebereich definieren

Ziel dieser Aufgabe ist es, die Eingabe eines nicht zulässigen Wertes durch den Anwender am NumericInput-Widget

zu verhindern.

1)In der Configuration View die Datei "OpcUaCsMap.uad" unter "Connectivity \ OpcUaCs" öffnen

2)Auf die globale Variable "SetTemperature" klicken

3)Im Eigenschaftsfenster unter "Wertebereich" (EU Range) folgendes parametrieren:

## Page 44

44ARBEITEN MIT MAPP VIEW TM611

Unteres LimitOberes Limit

2550

Abbildung 46: Der Wertebereich von "SetTemperature" ist in der OPC UA Default View definiert

Ergebnis:

Es werden beim Node Binding die Limits der OPC-UA-Variable verwendet.

Abbildung 47: Der Wertebereich von "SetTemperature" ist in der OPC UA Default View definiert

## Page 45

MEDIENDATEIEN 45
9 Mediendateien
Um eine Visualisierung optisch ansprechender aussehen zu lassen, können in mapp View Mediendateien eingefügt
werden.
9.1 Image-Widget
Mit dem Einfügen des mapp View Packages in die Logical View wird automatisch das Media Package angelegt. Dieses
dient zur logischen Verwaltung von Grafiken in der Visualisierung. Alle Dateien, die im Media Package abgelegt sind,
werden auf das Zielsystem übertragen.
Das Image-Widget unterstützt folgende Bildformate: .png, .jpg, .svg, .gif und .bmp.
Visualization \ mapp View \ Engineering \Organization of the HMI application \ Package "mapp View"
\ mapp View Media package
Beispiel:
Der Pfad, um Images zu referenzieren, beginnt immer mit dem Package-Namen "Media". Anschließend
folgt, falls vorhanden, der Name des Sub-Packages und am Ende der Dateiname des Images.
Beispiel für das Image "Test.png", welches direkt im Media Package abgelegt ist:
URL= "Media/Test.png"
Beispiel für das Bild "Test.png", welches in dem Sub-Package "SmallPictures" im Media Package abgelegt
ist:
URL = "Media/SmallPictures/Test.png"
Aufgabe: Image einfügen
Ziel dieser Aufgabe ist es, das B&R Logo auf jeder Visualisierungsseite anzuzeigen. Das Logo ist Teil des Automation
Studio Projektes "mappViewGettingStarted".
Um die Grafik darzustellen, wird auf "ContentTop" das Image-Widget eingefügt und parametriert.
1) In der Logical View den Ordner "Images" markieren
2) Den Ordner per Drag & Drop hierhin verschieben:
Logical View \ mapp View \ Resources \ Media
3) "ContentTop" öffnen
4) Aus dem Objektkatalog folgendes Widget einfügen und parametrieren:
Widget Layout \ Position Layout \ Size
Image 20; 20 180; 60
5) Unter "Appearance \ Image" auf das Feld "Default" klicken und den Auswahldialog öffnen
6) Unter "Media \ Images" die "Datei BuRLogo.png" auswählen
7) Auswahldialog mit OK schließen
8) Projekt kompilieren, übertragen und in der Visualisierung anzeigen

## Page 46

46ARBEITEN MIT MAPP VIEW TM611

Ergebnis:

Abbildung 48: Ordner Images im Media Package, Image-Widget auf "ContentTop"

Aufgrund der bereits angepassten Größe des Image-Widgets und der standardmäßig eingestellten Ei-

genschaft "sizeMode" = "contain", wird das Image-Widget vollständig mit dem Bild ausgefüllt.

Die möglichen Optionen für die Eigenschaft "sizeMode" sind in der Automation Help bei diesem Widget

beschrieben.

Abbildung 49: Visualisierung im Browser mit eingefügtem B&R Logo

## Page 47

MEDIENDATEIEN47

9.2SVG Symbol Bibliothek

mapp View stellt eine umfangreiche SVG Symbol Bibliothek zur freien Verwendung zur Verfügung.

Aufgabe: SVGSymbol Bibliothek einfügen

Ziel dieser Aufgabe ist das Einfügen der SVG Symbol Bibliothek, um die Gra-

fiken für nachfolgende Aufgaben nutzen zu können. Die Bibliothek wird im

Package "Media" eingefügt.

1)Unter "mapp View \ Resources" auf "Media" klicken

2)Aus dem Objektkatalog die Datei "SVG Symbols Package" einfügen

Die SVG Symbol Bibliothek wird erst zu einem späteren Zeit-

punkt im Projekt verwendet, zählt aber auch zu den Medien-

dateien und wird deshalb bereits hier eingefügt.

Abbildung 50: Im mapp View Projekt eingefügte

SVG Symbol Bibliothek

## Page 48

48ARBEITEN MIT MAPP VIEW TM611

10Benutzer-Rollen-System

Automation Studio stellt ein Benutzer-Rollen-System bereit, das von mapp View verwendet wird. Dieses System im-

plementiert das Verfahren von Role-Based-Access-Control (RBAC), das durch die ANSI Norm 359-2004 beschrieben ist.

10.1RBAC-Konzept

Die rollenbasierte Zugriffskontrolle basiert auf den Konzepten Benutzer, Rollen und Rechte. Rechte werden dabei Rol-

len zugeordnet und Rollen wiederum werden mit Benutzern verknüpft. Benutzer können gleichzeitig mehrere Rollen

einnehmen. Die direkte Zuordnung von Berechtigungen an Benutzer erfolgt nicht, da sich dies in der Praxis als unüber-

sichtlich und fehlerträchtig erwiesen hat.

Ein Benutzer repräsentiert dabei im System eine natürliche Person, die mittels Name, Vorname, etc. näher beschrieben

wird. Zusätzlich enthält ein Benutzer Informationen, die zur Authentifizierung an dem System verwendet werden. Mit

der Authentifizierung weist die natürliche Person dem System gegenüber nach, dass sie diejenige Person ist, die sie

vorgibt zu sein. Die weit verbreitetste Authentifizierungsmethode besteht aus einer eindeutigen Identifikation des

Benutzers (User-ID) und einem geheimen Passwort, das nur die natürliche Person und das System kennen darf.

Eine Rolle beschreibt in Ausübung welcher Aufgabe ein Benutzer mit einem System interagiert. Beispiele für Rollen

können sein: Administrator, Servicetechniker oder Maschinenbediener. Für die Ausübung unterschiedlicher Aufgaben

sind üblicherweise unterschiedliche Berechtigungen erforderlich. Dazu werden Rechte an Rollen vergeben. Ändert sich

der Aufgabenbereich einer natürlichen Person, ist damit nur mehr die Änderung der Zuordnung des entsprechenden

Benutzers zu den neuen Rollen notwendig, um dieser Person mit den für die neue Aufgabe erforderlichen Rechten

auszustatten.

Abbildung 51: Zusammenhänge im Benutzer-Rollen-System

Programming \ Access & Security \ User role system \ General information

Bei einem neuen Automation Studio Projekt sind keine Rollen bzw. Benutzer angelegt.

Einige Widgets benötigen spezielle Rechte, welche die Zuweisung der entsprechenden B&R Rolle(n) für

den Benutzer notwendig machen (siehe auch: Visualization \ mapp View \ Engineering \ Organization

of the HMI application \ mapp View configuration \ Server configuration \ Authentication on the mapp

View server)

## Page 49

BENUTZER-ROLLEN-SYSTEM 49
10.2 Benutzer-Rollen-System anwenden
Mehrteilige Aufgabe - Benutzer-Rollen-System
10.3 "Schreibrechte auf OPC UA Node einschränken"
1) "Aufgabe: Benutzer-Rollen-System konfigurieren" auf Seite 49
2) "Aufgabe: OPC UA DefaultView Konfiguration - Rollen berechtigen" auf Seite 50
10.4 "Authentifizierung in der Visualisierung"
1) "Aufgabe: Startup Benutzer auf "force login" ändern" auf Seite 51
2) "Aufgabe: Widgets zur Authentifizierung in der Visualisierung" auf Seite 52
10.3 Schreibrechte auf OPC UA Node einschränken
Neue Rollen und Benutzer hinzufügen
Programming \ Access & Security \ User role system \ Configuration \ Automation Studio configuration
Bearbeitung von Rollen im Benutzer-Rollen-System
•
Bearbeitung von Benutzern im Benutzer-Rollen-System
•
Aufgabe: Benutzer-Rollen-System konfigurieren
Ziel dieser Aufgabe ist es, den Schreibzugriff auf "SetTemperature" durch nicht berechtigte Benutzer zu verhindern.
Dazu wird das Benutzer-Rollen-System entsprechend konfiguriert.
1) In der Configuration View unter "AccessAndSecurity \ UserRoleSystem" eine Rollendatei aus dem Objektkatalog
einfügen
2) "Role.role" öffnen
3) Mit Rechtsklick zwei weitere neue Rollen hinzufügen
4) Rollen zu "Operator", "Service" und "Observer" umbenennen
5) "Role.role" speichern, damit die Rollen gesichert sind
6) In der Configuration View unter "AccessAndSecurity \ UserRoleSystem" eine Userdatei aus dem Objektkatalog
einfügen
7) "User.user" öffnen
8) Mit Rechtsklick zwei weitere neue Benutzer hinzufügen und parametrieren:
Name Password Assigned role
UserOperator 5555 Operator Darf die Maschine bedienen und bestimmte Werte ändern.
UserService 9999 Service Hat erweiterte Rechte zum Einrichten und Warten der Maschine.
UserObserver 0000 Observer Kann den Zustand der Maschine beobachten und hat einge-
schränkte Schreibrechte.
9) User.user speichern, damit die Benutzer gesichert sind
Es dürfen beliebige andere Benutzernamen und Passwörter verwendet werden.

## Page 50

50ARBEITEN MIT MAPP VIEW TM611

Ergebnis:

Im Benutzer-Rollen-System gibt es nun drei Rollen und drei Benutzer.

Abbildung 52: Neue Rollen "Operator", "Service" und "Observer" und neue Benutzer "UserOperator", "UserService" und "UserObserver"

OPC-UA-Konfiguration

In der OPC UA DefaultView Konfiguration ("UaDvConfig.uadcfg") werden die Default Berechtigungen für eine oder

mehrere Rollen eingestellt. Diese Berechtigungen gelten für alle darunterliegenden Variablen, solange für sie nichts

anderes eingestellt wird. Durch das Hinzufügen der verschiedenen Rollen können die Berechtigungen jeder einzelnen

Rolle definiert werden.

Vor der Vergabe von Schreibrechten ist zu überprüfen, ob die OPC-UA-Schreibrechte nur für einige wenige

OPC-UA-Variablen eingeschränkt werden soll, oder für die Mehrheit der Variablen.

Sollte die Einschränkung der Schreibrechte nur für wenige OPC-UA-Variablen gelten, ist es am einfachs-

ten, diese Variablen auszuwählen und dort die Schreibrechte einzuschränken.

Aufgabe: OPC UA DefaultView Konfiguration - Rollen berechtigen

Ziel dieser Aufgabe ist es, in der OPC UA DefaultView Konfiguration die Berechtigungen für die Rollen zu definieren.

Außerdem wird für die Variable "SetTemperature" definiert, dass nur Benutzer mit der Rolle "Service" oder "Operator"

das Recht haben, diese Variable zu überschreiben. Benutzer mit der Rolle "Observer" kann keine Werte verändern.

1)In der Configuration View unter "Connecitivity \ OpcUaCs" die Datei "UaDvConfig.uadcfg" öffnen

2)Unter "DefaultRolePermissions \ Role 1" auf das Feld "Name" klicken und im Dropdown "Operator" auswählen

3)Auf den Eintrag "DefaultRolePermissions" klicken

4)Die Rolle "Operator" für die 1. Rolle zuweisen

5)Die Zurgiffsrechte unter Permissions für folgende Attribute aktivieren:

Browse (Enabled)

°

Read (Enabled)

°

Write (Enabled)

°

Call (Enabled)

°

ReadRolePermissions (Enabled)

°

ReadHistory (Enabled)

°

6)Weitere Rollen "Service" und "Observer" hinzufügen und die Zugriffsrechte entsprechend konfigurieren.

In diesem Training werden die Schreibrechte nur für die Rolle "Observer" eingeschränkt.

## Page 51

BENUTZER-ROLLEN-SYSTEM51

Ergebnis:

Bis auf die Rolle "Observer" haben alle Rollen Schreibrechte.

Abbildung 53: Schreibrechte für alle Rollen in OPC UA DefaultView Konfiguration definieren

10.4Authentifizierung in der Visualisierung

Damit die eingestellten Berechtigungen für die jeweiligen Benutzer zur Wirkung kommen, muss sich ein Benutzer dem

System gegenüber authentifizieren. Dafür stehen Widgets zur Verfügung, die für das Login und das Logout sowie für

die Anzeige detaillierter Login-Information sorgen.

Weiters muss in der mapp View Server Konfiguration der Startup Benutzer von "anonymous token" auf "force login"

geändert werden.

Visualization \ mapp View \ Engineering \ Organization of the HMI application \ mapp View configuration

\ Server configuration

Visualization \ mapp View \ Engineering \ Organization of the HMI application \ mapp View configuration

\ Server configuration \ Authentication on the mapp View server

Login am Client erzwingen

Ist eine Anmeldung eines beliebigen Benutzers an der mapp View Visualisierung unbedingt erforderlich, ist in der mapp

View Server Konfiguration als Startup Benutzer zu projektieren."force login"

Loggt sich ein Benutzer aus, wechselt die Visualisierung automatisch zur Authentifizierungsseite.

Aufgabe: Startup Benutzer auf "force login" ändern

Ziel dieser Aufgabe ist es, dass sich Nutzer mit eingestellten Berechtigungen gegenüber dem System authentifizieren

können. Dafür werden diese Widgets verwendet: Login, LogoutButton, LoginInfo. Zusätzlich wird ein Image-Widget

zur Anzeige eines Symbols aus der zuvor eingefügten SymbolLib verwendet.

1)In der Configuration View unter "mapp View" die mapp View Konfiguration "Config.mappviewcfg" öffnen

2)Unter "Server configuration \ Startup User" die Dropdown-Option "force login" wählen

3)Projekt kompilieren, übertragen und in Visualisierung anzeigen

## Page 52

52ARBEITEN MIT MAPP VIEW TM611

Ergebnis:

Bei einem Neustart des Clients im Browser wird ein Login Dialog angezeigt, in welchem sich der Benutzer

mit seinem Namen und Passwort authentifizieren muss.

Abbildung 54: Visualisierung im Browser mit Aufforderung zur Authentifizierung

Aufgabe: Widgets zur Authentifizierung in der Visualisierung

Ziel dieser Aufgabe ist es, dass sich Nutzer mit eingestellten Berechtigungen gegenüber dem System authentifizieren

können. Dafür werden diese Widgets verwendet: Login, LogoutButton, LoginInfo. Zusätzlich wird ein Image-Widget

zur Anzeige eines Symbols aus der zuvor eingefügten SymbolLib verwendet.

1)"ContentTop" öffnen

2)Aus dem Objektkatalog folgende Widgets einfügen und parametrieren:

WidgetCommon \ NameAppearance \ ImageLayout \ PositionLayout \ Size

ImageImageLoginSymbolMedia/SymbolLib/User/User.svg11; 83030; 38

WidgetCommon \ NameAppearance \ TextLayout \ PositionLayout \ Size

LabelLabelLoginInfoLogged in as:15; 880140; 30

WidgetCommon \ NameLayout \ PositionLayout \ Size

LoginInfoLoginInfoUser15; 1030230; 30

WidgetCommon \ NameAppearance \ TextLayout \ PositionLayout \ Size

LogoutButtonLogoutButtonUserLogout50; 1030100; 30

3)"ContentServicePage" öffnen

4)Aus dem Objektkatalog folgendes Widget einfügen und parametrieren:

WidgetCommon \ NameLayout \ PositionLayout \ Size

LoginLoginUser40; 50300; 160

5)Projekt kompilieren, übertragen und in Visualisierung anzeigen

## Page 53

BENUTZER-ROLLEN-SYSTEM53

Ergebnis:

Nach dem Einloggen mit dem Benutzer "UserOperator" mit Passwort "5555" bzw. "UserService" mit Pass-

wort "9999" können auf der "MainPage" Eingaben im NumericInput-Widget durchgeführt werden. Für den

Benutzer "UserObserver" mit Passwort "0000" ist die Eingabe durch die fehlenden Schreibrechte an der

OPC UA Node automatisch deaktiviert.

Abbildung 55: Visualisierung im Browser nach erfolgtem Login

## Page 54

54 ARBEITEN MIT MAPP VIEW TM611
11 Lokalisierung
Lokalisierung bedeutet bei mapp View die Anpassung von Inhalten einer Visualisierung an lokale, sprachliche und kul-
turelle Gegebenheiten. mapp View ermöglicht zur Lokalisierung von Visualisierungen die Anpassung von Texten sowie
die Anpassung von Einheiten zur Laufzeit.
Für die Lokalisierung von Texten wird das Textsystem verwendet; für die Anpassung von Einheiten wird das Einheiten-
system eingesetzt.
11.1 Textsystem
Automation Studio stellt für die Lokalisierung von Texten das Textsystem bereit. Im Textsystem werden die verschie-
denen Sprachen verwaltet. Die Darstellung des Textsystems ist zweidimensional, siehe nachfolgende Tabelle.
In einer Dimension werden Bedeutungen (Semantiken) durch einen eindeutigen Identifier repräsentiert, während die
andere Dimension durch alle Sprachen, für die es eine textuelle Repräsentation dieser semantischen Aussagen gibt,
gebildet wird.
Jede Zeile stellt eine semantische Aussage mit ihren Texten in den jeweiligen Sprachen dar.
Jede Spalte stellt die Texte einer einzelnen Sprache für die verschiedenen semantischen Aussagen dar.
In jeder Zelle ist damit ein Text enthalten für die semantische Aussage (Identifier in der ersten Spalte) in der Sprache,
die durch diese Tabellenspalte repräsentiert wird.
Beispiel:
Das Beispiel in der Tabelle zeigt die Bedeutungen "Abbrechen", "Ja" und "Nein" in verschiedenen Spra-
chen. Diese Bedeutungen sind durch die Identifier "Text_Cancel", "Text_Yes" und "Text_No" eindeutig be-
zeichnet.
Zur Semantik "abbrechen" gibt es einen Text in deutscher Sprache ("Abbrechen"), einen Text in englischer
Sprache ("Cancel"), einen Text in französischer Sprache ("Annuler") und einen Text in spanischer Sprache
("Cancelar").
Identifier de en fr es
Text_Cancel Abbrechen Cancel Annuler Cancelar
Text_Yes Ja Yes Oui Si
Text_No Nein No Non No
Tabelle 2: Ein Beispiel für lokalisierte Texte in schematischer Darstellung
Wird in einem System mit lokalisierbaren Texten mit textuellen Aussagen gearbeitet, müssen dabei die Identifier für
die Bedeutungen dieser textuellen Aussagen verwendet werden. Wird das System im sprachlichen Kontext "Deutsch"
verwendet, ersetzt das System die Identifier für die Bedeutungen durch die Texte für die deutsche Sprache. Dazu wer-
den für die Identifier die Texte aus der Tabellenspalte "de" verwendet. Wird dasselbe System im sprachlichen Kontext
"Englisch" verwendet, dann ersetzt das System die Identifier für die Bedeutungen durch die Texte in englischer Spra-
che (Tabellenspalte "en").

## Page 55

LOKALISIERUNG 55
11.2 Identifier
Ein Identifier bezeichnet eine semantische Aussage, die als Text in unterschiedlichen Sprachen repräsentiert wird. Da in
einem Automatisierungssystem eine große Menge von semantischen Aussagen enthalten sein kann, ist es notwendig,
diese strukturieren zu können, um zu verhindern, dass verschiedenen semantischen Aussagen derselbe Identifier ge-
geben wird. Dies ist insbesondere deshalb wichtig, weil semantische Aussagen unabhängig voneinander von verschie-
denen Personen definiert werden können. So kann beispielsweise der Entwickler der Automatisierungsapplikation se-
mantische Aussagen für die Verwendung innerhalb der Logger-Einträge definieren und unabhängig davon kann der
Entwickler der Visualisierung semantische Aussage definieren, die in den Visualisierungselementen verwendet werden.
Das Textsystem stellt dazu die Möglichkeit bereit, Identifier für semantische Aussagen in Namespaces zu strukturie-
ren. Ein Identifier für eine semantische Aussage setzt sich daher aus mehreren Teilen zusammen:
Identifier=Namespace+NamespaceSeparator+Text_ID
Ein Namespace kann wiederum Teil eines anderen Namespaces sein. Namespaces können also hierarchisch struktu-
riert sein. Für einen Namespace gilt:
Namespace=NamespaceName [ + NamespaceSeparator+Namespace]
Identifier werden auch als vollständig qualifizierte Text_IDs bezeichnet. Für die Gestaltung von Text_IDs, Namespace-
Name gibt es Regeln, die in der Automation Help ausführlich beschrieben sind. Der NamespaceSeparator ist ein ein-
zelnes Zeichen, das ebenfalls in der Automation Help beschrieben ist.
Beispiel:
Nachfolgend ein paar Beispiele für Identifier (vollständig qualifizierte Text_IDs).
1) Texts/AppEvents/Internal/FatalError
2) Texts/Alarms/Alarm1
3) Texts/Program/Alarme/AlarmID1
Bei allen Beispielen ist der NamespaceSeparator das Zeichen ‚/‘. Die Text_IDs in den Beispielen sind "Fa-
talError", "Alarm1" und "AlarmID1". Im ersten Beispiel gibt es einen Namespace "Texts", in dem es einen
weiteren Namespace "AppEvents" gibt und dieser beinhaltet noch den Namespace "Internal".
11.3 Konfiguration der Projektsprachen
Für ein Automation Studio Projekt ist zu definieren, für welche Sprachen Texte zu den semantischen Aussagen bereit-
gestellt werden können.
Neben dem Festlegen der Projektsprachen die zur Verfügung stehen sollen, muss die "Design Language" definiert
werden. Die "Design Language" definiert in welcher Sprache die Texte während der Projektierung in Automation Studio
dargestellt werden.
Programming \ Text system \ Configuring project languages
11.4 Konfiguration der Textsystem-Konfigurationsdatei
Aus den Sprachen, die im Automation Studio Projekt konfiguriert sind, kann für jede Konfiguration des Projektes fest-
gelegt werden, welche der Sprachen auf das Zielsystem zu dieser Konfiguration übertragen werden sollen.
Des Weiteren wird die "System-Sprache" sowie die "Fallback-Sprache" definiert. Die "System-Sprache" legt fest, in wel-
cher Sprache die Texte auf dem Target per Default dargestellt werden sollen. Die "Fallback-Sprache" definiert in wel-
cher Sprache die Texte dargestellt werden sollen, falls keine Texte für die "System-Sprache" zur Verfügung stehen.
Programming \ Text system \ Details about the configuration file

## Page 56

56 ARBEITEN MIT MAPP VIEW TM611
11.5 Textdateien in mapp View
In mapp View können lokalisierte Texte aus dem Textsystem verwendet werden. Um eigene Texte in mapp View ein-
bringen zu können, sind Textdateien zu verwenden. Werden eigene Texte über Textdateien eingebracht, werden diese
vom Textsystem zur Verwendung bereitgestellt.
Eigene lokalisierte Texte und das Textsystem sind die Basis dafür, dass eine mapp View Visualisierung mit Texten in
unterschiedlichen Sprachen dargestellt werden kann. Um die Sprache für eine Visualisierung umschalten zu können,
stellt mapp View entsprechende Möglichkeiten bereit.
Für die Verwendung einer Textdatei in einer mapp View Visualisierung muss in dieser der Namespace
"IAT" eingetragen sein. Dieser Namespace ist für mapp View reserviert und wird zur Laufzeit benötigt,
um die Texte der Visualisierung vom Textsystem auszulesen. Dadurch sind schnellere Bildwechsel bzw.
Textumschaltungen möglich.
Visualization \ mapp View \ Engineering \Text system \ Adding text files
11.6 Textsystem anwenden
Mehrteilige Aufgabe - Textsystem
Texte für die manuelle Navigation
1) "Aufgabe: Projektsprachen erstellen und Texte anlegen" auf Seite 56
2) "Aufgabe: Texte auf Widgets referenzieren" auf Seite 57
3) "Aufgabe: Textsystem-Konfigurationsdatei und LanguageSelector-Widget einfügen" auf Seite 58
Texte für weitere Widgets
4) "Aufgabe: Sprachumschaltung Label-Widget und LogoutButton-Widget" auf Seite 59
Aufgabe: Projektsprachen erstellen und Texte anlegen
Ziel dieser Aufgabe ist das Einfügen der Sprachkonfigurationsdatei "Project.language". Die "Design Language" bleibt
wie per Default eingestellt "Englisch". Anschließend werden im Tabelleneditor die Texte für die NavigationButton-Wid-
gets mit einer eindeutigen Text ID in Englisch und Deutsch angelegt.
1) In der Logical View auf den Projektnamen klicken
2) Aus dem Objektkatalog die Projektsprachen einfügen
3) "Project.language" öffnen
4) Den Eintrag "fr" löschen und Datei speichern
5) Unter "mappView \ Resources" auf "Texts" klicken
6) Aus dem Objektkatalog eine "mapp View LocalizableTexts.tmx" Datei einfügen
7) Die Datei zu "VisualizationTexts" umbenennen
8) "VisualizationTexts.tmx" öffnen und bearbeiten:
Text ID German (de) English (en)
1 MainPage Start Main
2 ServicePage Service Service
3 InfoPage Information Info

## Page 57

LOKALISIERUNG57

Ergebnis:

Für die manuelle Navigation sind die Buttontexte auf Deutsch und Englisch definiert.

Abbildung 56: mapp View Localizable Text Datei mit definierten Texten

Aufgabe: Texte auf Widgets referenzieren

Ziel dieser Aufgabe ist das Referenzieren der Texte auf die NavigationButton-Widgets.

1)"ContentLeft" öffnen

2)Auf "MainPageButton" klicken

3)Im Eigenschaftsfenster unter "Appearance \ text" auf das Feld "Default" klicken und den Auswahldialog öffnen

4)Die Text_ID "MainPage" auswählen und OK klicken

5)Den Vorgang für "ServicePage" und "InfoPage" wiederholen

Ergebnis:

Die Texte sind mit den NavigationButton-Widgets verbunden.

Abbildung 57: Zuweisung der lokalisierten Texte auf die NavigationButton-Widgets über den Auswahldialog

LanguageSelector-Widget

Das LanguageSelector-Widget dient zur Auswahl der Sprache in der Visualisierung. Das eingefügte LanguageSelec-

tor-Widget stellt Sprachen in einer DropDown Liste zur Verfügung.

Visualization \ mapp View \ Widgets \ System \ LanguageSelector

## Page 58

58ARBEITEN MIT MAPP VIEW TM611

Aufgabe: Textsystem-Konfigurationsdatei und LanguageSelector-Widget einfügen

Ziel dieser Aufgabe ist das Einfügen und Bearbeiten der Textsystem-Konfigurationsdatei, damit die Texte auf das

Zielsystem übertragen werden. Außerdem wird in der Visualisierung das LanguageSelector-Widget eingefügt, um die

Sprache während der Laufzeit umzuschalten.

1)In der Configuration View auf "TextSystem" klicken

2)Aus dem Objektkatalog die "Textsystem Configuration" einfügen

3)"TC.textconfig" öffnen und bearbeiten:

System languageFallback languageTarget languagesTmx files for target

enenenmappView.Resources.Texts.Visualiza-

detionTexts.tmx

4)"ContentServicePage" öffnen

5)Aus dem Objektkatalog folgendes Widget einfügen und parametrieren:

WidgetLayout \ PositionLayout \ Size

LanguageSelector220; 40160; 30

6)Projekt kompilieren, transferieren und im Browser anzeigen

Ergebnis:

Aufgrund der Referenzierung der Textdateien in "TC.textconfig" werden die Texte zur Laufzeit auf das

Zielsystem geladen.

Abbildung 58: Konfigurierte TextConfig Datei

Mithilfe des LanguageSelector-Widgets kann zwischen den Projektsprachen gewählt werden.

Abbildung 59: Visualisierung im Browser mit lokalisierten Texten für die manuelle Navigation

## Page 59

LOKALISIERUNG59

Aufgabe: Sprachumschaltung Label-Widget und LogoutButton-Widget

Ziel dieser Aufgabe ist es, die Texte für das Label-Widget (mit der Info über den eingeloggten Benutzer) und das Lo-

goutButton-Wigdets zu lokalisieren, um anschließend mithilfe des LanguageSelector-Widgets zwischen Deutsch und

Englisch umschalten zu können.

Ergebnis:

In der Datei "VisualizationTexts.tmx" werden alle weiteren Texte der Visualisierung auf Deutsch und Eng-

lisch eingetragen. Die Widgets sind mit den passenden Texten verbunden.

Abbildung 60: Lokalisierte Texte für Label-Widget und LogoutButton-Widget

## Page 60

60 ARBEITEN MIT MAPP VIEW TM611
11.7 Einheitensystem
Mit Automation Studio wird dem Anwender ein integriertes Einheitensystem mit automatischer Einheitenumrechnung
zur Verfügung gestellt. Für die Lokalisierung von Einheiten stehen dem Anwender die Maßsysteme "metric", "imperial"
und "imperial-us" sowie mehr als 1400 Einheiten zur Verfügung.
Bei einem OPC UA Node kann neben dem Wertebereich auch die Engineering Unit konfiguriert werden. Die Engineering
Unit eines Nodes gibt an, in welcher physikalischen Einheit der Wert zu interpretieren ist.
Bei einem Widget kann definiert werden, in welcher Einheit der Wert eines gebundenen OPC UA Nodes für das gewählte
Maßsystem darzustellen ist. In der Automation Help sind alle Einheiten und Common Codes (Bsp. Grad Celsius = CEL)
aufgelistet. Der Common Code wird in den Eigenschaften des Widgets benötigt. Wechselt man in der Visualisierung
das Maßsystem, so wird der Wert und die Einheit automatisch umgerechnet.
Programmierung \ Unit System \ Verfügbare Standardeinheiten
In Automation Studio können auch benutzerdefinierte Einheiten erstellt werden, siehe: Visualisierung \
mapp View \ Leitfäden \ FAQ \ Widgets Anwendungen \ Benutzerdefinierte Einheit 1/10°
11.8 Einheitensystem anwenden
Mehrteilige Aufgabe - Einheitensystem
Wert mit Einheit darstellen und Maßsystem umschalten
1) "Aufgabe: OPC-UA-Konfiguration - Engineering Unit zuweisen" auf Seite 60
2) "Aufgabe: Einheiten an Widgets anzeigen" auf Seite 61
3) "Aufgabe: MeasurementSystemSelector-Widget einfügen" auf Seite 62
OPC UA Node Engineering Unit "Degree Celsius" anhängen
In der OPC UA Default View kann einem OPC UA Node nach der Aktivierung eine Einheit konfiguriert werden.
Visualization \ mapp View \ Guides \ Getting started \ Binding widgets to data \ Displaying a value and
unit
Aufgabe: OPC-UA-Konfiguration - Engineering Unit zuweisen
Ziel dieser Aufgabe ist es, für die OPC UA Nodes "CurrentTemperature" und "SetTemperature" die Einheit Grad Celsius
(°C) zu definieren.
1) In der Configuration View unter "Connectivity \ OpcUaCs" auf "OpcUaCsMap.uad" klicken
2) Auf die globale Variable "SetTemperature" klicken
3) Aus dem EngineeringUnit Kagalog die Einheit "degree Celsius" auswählen
4) Per Doppelklick die Einheit zuweisen
5) Den Vorgang für die globale Variable "CurrentTemperature" wiederholen
Werden mehrere OPC-UA-Variablen gleichzeitig selektiert, kann eine Einheit gleichzeitig auf mehrere
OPC-UA-Variablen zugewiesen werden.

## Page 61

LOKALISIERUNG61

Ergebnis:

Die Variablen "SetTemperature" und "CurrentTemperature" haben die Engineering Unit "degree Celsius".

Abbildung 61: OPC UA Node "CurrentTemperature" mit der Einheit Grad Celsius (°C)

Aufgabe: Einheiten an Widgets anzeigen

Ziel dieser Aufgabe ist es, dass die Einheit der Variable "SetTemperature" und "CurrentTemperature" an beiden Nume-

ricOutput-Widgets angezeigt wird. Dafür werden an den Widgets die Einheiten für die drei Maßsysteme definiert, um

später die Einheit umschalten zu können. Für jedes Maßsystem ("metric", "imperial", "imperial-us") muss eine darzu-

stellende Einheit angegeben werden.

Metric: Degree Celsius (°C)

•

Imperial: Degree Fahrenheit (°F)

•

Imperial-US: Degree Fahrenheit (°F)

•

1)"ContentMainPage" öffnen

2)Auf das NumericInput-Widget mit Node Binding klicken

3)Im Eigenschaftsfenster unter "Appearance \ unit" auf das Feld "Default" klicken und den Auswahldialog öffnen

4)Für die drei Maßsysteme die Einheiten definieren

5)Die Eigenschaft "unitAlign" auf "right" ändern

6)Die Schritte 3 bis 5 für das NumericOutput-Widget mit Node Binding wiederholen

## Page 62

62ARBEITEN MIT MAPP VIEW TM611

Ergebnis:

Für das NumericInput-Widget und das NumericOutput-Widget mit Node Binding sind die Einheiten für

alle drei Maßsysteme definiert.

Abbildung 62: Definineren der Unit Property

MeasurementSystemSelector-Widget

Das MeasurementSystemSelector-Widget ermöglicht dem Benutzer ein Maßsystem auszuwählen. Das eingefügte Me-

asurementSystemSelector-Widget stellt die Maßsysteme in einer DropDown Liste zur Verfügung.

Visualization \ mapp View \ Widgets \ System \ MeasurementSystemSelector

Aufgabe: MeasurementSystemSelector-Widget einfügen

Ziel dieser Aufgabe ist es, dass die Einheitenumschaltung in der Visualisierung über das MeasurementSystemSelec-

tor-Widget funktioniert.

1)"ContentServicePage" öffnen

2)Aus dem Objektkatalog folgendes Widget einfügen und parametrieren:

WidgetLayout / PositionLayout / Size

MeasurementSystemSelector280; 40160; 30

3)Projekt kompilieren, transferieren und im Browser anzeigen

## Page 63

LOKALISIERUNG63

Ergebnis:

Der Unterschied zwischen Value Binding und Node Binding ist nun sichtbar, da das Node Binding die

Datenweitergabe (inkl. Wert, Einheit und Grenzen) nutzt. Die Engineering Unit ist zwar degree Celsius,

doch durch die konfigurierten Maßeinheiten kann zwischen diesen Einheiten gewechselt werden.

Beim  findet zwar auch eine scheinbare "Einheitenumschaltung" von °C auf °F statt, dochValue Binding

der Wert wird .nicht ungerechnet

Abbildung 63: Visualisierung im Browser mit Darstellung von Einheiten im Node-Binding

## Page 64

64ARBEITEN MIT MAPP VIEW TM611

12Ereignisse und Aktionen

Um das Verhalten einer Visualisierung individueller projektieren zu können, stellt mapp View Ereignisse und Aktionen

zur Verfügung. Dabei werden in mapp View Eventbindings konfiguriert.

Eventbindings reagieren auf ein Ereignis () mit ei-Event

ner oder mehreren Aktionen ().Action

Dabei können zusätzliche Informationen aus der Cli-

ent-Session oder dem OPC-UA-System zur Auswertung

oder Entscheidung hinzugezogen werden. Diese zusätz-

lichen Informationen werden als verwaltet.Operanden

Jeder Aktion kann eine Bedingung () vorange-Condition

stellt werden, unter der die jeweilige Aktion ausgeführt

werden soll. Der in der Abbildung rot dargestellte Block

aus Condition und Action wird auch als Resulthandler

bezeichnet.

Abbildung 64: Struktur eines Eventbindings im grafischen Editor

Neben der Konfiguration von Eventbindings in der XML Syntax bietet mapp View auch die Möglichkeit Eventbindings

in einem grafischen Editor zu verwalten. Es kann zwischen beiden Ansichten gewechselt werden. EventBinding Dateien

sind in der Configuration View im mappView Ordner zu finden.

12.1Ereignisse

Ein Ereignis ist ein Vorkommnis, das als Auslöser für eine Reaktion auf dieses Vorkommnis verwendet werden kann.

mapp View stellt dem Entwickler einer Visualisierung bereits mehrere Typen von Ereignissen bereit.

Visualization \ mapp View \ Engineering \ Events and actions \ Event

OPC UA Ereignis

•

Session Ereignis

•

Client System Ereignis

•

Widget Ereignis

•

OPC UA System Ereignis

•

OPC UA Ereignisse

mapp View definiert Ereignisse um über die Änderungen von Werten aus der Automatisierungsapplikation zu infor-

mieren. Dazu stellt mapp View Ereignisse vom Typ "opcUa.Event" mit dem Namen "ValueChanged" bereit.

Session Ereignisse

mapp View definiert Ereignisse um Vorkommnisse in einer Client-Session zu erkennen. Session Ereignisse werden im

TM671 behandelt z. B. "ValueChanged" einer Session Variablen.

Client System Ereignisse

Client System Ereignisse definieren Vorkommnisse, die sich auf einem Client ereignen. Dazu zählen z. B. das

"KeyBoardEvent" oder das "ContentLoadedEvent".

Widget Ereignisse

Widget Ereignisse informieren über Vorkommnisse in konkreten Widget-Instanzen. Unterschiedliche Widgettypen

können über verschiedene Vorkommnisse in Form von Ereignissen informieren. Ein Beispiel dafür ist das Click-Ereignis

des Button-Widgets.

OPC UA System Ereignisse

mapp View definiert Ereignisse, um über Zustandsänderungen am OPC-UA-Server zu informieren. Die bereitgestellten

Ereignisse sind "Connected" und "Disconnected".

## Page 65

EREIGNISSE UND AKTIONEN 65
12.2 Aktionen
Eine Aktion ist eine durch das Auftreten eines Ereignisses ausgelöste Reaktion. Aktionen werden nach dem jeweiligen
Anbieter von Aktionen zusammengefasst. In mapp View sind folgende Anbieter von Aktionen verfügbar:
OPC UA
•
Session
•
Client
•
Widgets
•
Jeder Anbieter definiert eine oder mehrere Gruppen von Aktionen.
Der Anbieter "OPC UA" stellt die Gruppe "opcUa.NodeAction" und der Anbieter "Widgets" die Gruppe "widgets.brea-
se.<WidgetType>" zur Verfügung.
Die Anbieter "Session" und "Client" sind Themen im TM671. In diesem Trainingsmodul wird nur auf die Anbieter "OPC
UA" und "Widgets" eingegangen.
Jede Gruppe definiert eine oder mehrere Aktionen.
Visualization \ mapp View \ Engineering \ Events and actions \ Action
OPC UA Aktionen
•
Session Aktionen
•
Client Aktionen
•
Widget Aktionen
•
OPC UA Aktionen
Für den Themenbereich OPC UA werden Aktionen zur Verfügung gestellt, die auf OPC UA Nodes wirken. Ein Beispiel
dafür ist SetValue Number.
Session Aktionen
In mapp View können Aktionen für die Client-Session definiert werden, z. B. SetValueNumber, TimerAction.Start.
Client Aktionen
Es gibt Aktionen, die von mappView für einen Client zur Verfügung gestellt werden. Dazu zählen neben Aktionen wie
OpenDialog oder CloseDialog auch SetLanguage oder SetMeasurementSystem.
Widget Aktionen
Für Widgets werden Aktionen zur Verfügung gestellt, die auf Instanzen von Widgets wirken.
Für jeden Widgettyp stehen typspezifische Aktionen zur Verfügung. Um über die jeweils verfügbaren Aktionen zu einem
Widgettyp weitere Details zu erfahren, kann die Widget-Dokumentation zu Rate gezogen werden. Ein Beispiel dafür
ist SetVisible.

## Page 66

66ARBEITEN MIT MAPP VIEW TM611

12.3Operanden

Einen Operanden kann man sich wie eine lokale Variable innerhalb eines EventBindings vorstellen. Diese lokale Variable

muss mit einem Namen und einem Datentyp erstellt werden.

Beim Auftreten des Ereignisses, für das das Eventbining erstellt wurde, wird zunächst eine lesende Aktion ausgeführt,

die den Operanden mit einem Wert initialisiert.

Anschließend kann dieser Wert dann beispielsweise in der Bedingung eines Resulthandlers verwendet werden.

In mapp View stehen fünf Arten von Operanden zur Verfü-

gung, die über die Toolbox in den grafischen Editor einge-

fügt werden können:

OPC UA

•

OPC UA System

•

Session

•

SessionTimer

•

Widgets

•

Visualisierung \ mapp View \ Engineering \ Ereignisse und Aktionen \ Operand

:Beispiel

1)Es wird ein Session Operand

verwendet

2)Der Name des Operanden ist

"CurrentPage"

3)Datentyp des Operanden ist

"ANY_STRING"

4)Wert, der gelesen wird,

ist die Systemvariable

"clientInfo.currentPageId"

5)Verwendung des Operanden

in der Bedingung des Resul-

Abbildung 65: Beispiel für die Vewendung eines Operanden im grafischen Editor

thandlers

Mit Klick auf das LogoutButton-Widget wird der Benutzer abgemeldet. Wenn sich der Nutzer während

des Abmeldens auf der "ServicePage" befindet, wird automatisch zur "MainPage" navigiert.

In einem EventBinding können auch mehrere Operanden erstellt und verwendet werden.

## Page 67

EREIGNISSE UND AKTIONEN67

12.4Ereignisse, Aktionen und Operanden anwenden

Mehrteilige Aufgabe - Ereignisse, Aktionen, Operanden

1)"Aufgabe: Sollwert des OPC UA Nodes per Button Klick zurücksetzen" auf Seite 68

2)"Aufgabe: Image-Widget bei Erreichen eines Wertes ein- und ausblenden" auf Seite 69

3)"Aufgabe: Automatischer Seitenwechsel nach Logout " auf Seite 72

12.5Wert setzen

Ereignisse projektieren

Für Widgets können Ereignisse im Eigenschaftsfenster des Content Editors

projektiert werden. Hierfür muss auf die Darstellung "Ereignisse" gewechselt

werden.

Beim Erstellen eines neuen Ereignisses für ein Widget wird erstmalig eine

neue EventBinding Datei in der Configuration View erzeugt und die Quelle des

Ereignisses eingetragen. Die EventBinding Datei wird automatisch in der Vi-

sualisierung (.vis) referenziert.

Abbildung 66: Erzeugen eines neuen Widget

Ereignisses aus dem Content Editor

EventBinding Datei

Die geöffnete EventBinding Datei enthält bereits eine automatisch generierte ID, welche der ContentId entspricht. Das

EventBinding wird mit der <Source> vorbelegt.

Für Widget Ereignisse setzt sich die EventBinding ID immer aus der Referenz des Contents, des Widgets und des Wid-

get Ereignisses zusammen. Diese ID wird nach dem Speichern der EventBinding Datei im Widget unter "Ereignisse"

angezeigt.

Beispiel:

Der Dateiname der EventBinding Datei ist "ContentMainPage.eventbinding". Klickt man in den weißen Be-

reich des grafischen Editors, sieht man die ID der EventBinding Datei; "ContentMainPage_eventbinding".

In der EventBinding Datei befindet sich ein EventBinding mit der ID

"ContentMainPage.ButtonSetToDefault.Click".

Abbildung 67: Geöffnete EventBinding Datei mit einem EventBinding darin

## Page 68

68ARBEITEN MIT MAPP VIEW TM611

Aufgabe: Sollwert des OPC UA Nodes per Button Klick zurücksetzen

Ziel dieser Aufgabe ist es, den Sollwert des OPC UA Nodes "SetTemperature" durch den Klick auf einen Button auf den

Default-Wert 35 zu setzen. Dafür wird für ein bestehendes Button-Widget ein Click-Ereignis konfiguriert.

1)"ContentMainPage" öffnen

2)Auf "Button1" klicken und das Widget parametrieren:

Appearance \ textCommon \ NameLayout \ Size

Set to defaultButtonSetToDefault140; 60

Mit der neuen ID "ButtonSetToDefault" kann dieser im EventBinding nachträglich leichter identifiziert werden. An-

schließend kann noch der Text des Buttons lokalisiert werden, sodass man zwischen Deutsch und Englisch um-

schalten kann.

3)Im Eigenschaftsfenster auf "Ereignisse" (Blitz-Icon) klicken

4)Auf das Feld "Click" klicken. Dadurch wird in der Configuration View eine neue EventBinding Datei erstellt.

5)Das EventBinding aufklappen

6)Aus dem Objektkatalog im Bereich "Actions" eine "opcUa.NodeAction" einfügen

Abbildung 68: Projektieren der OPC UA Aktion

7)Die opcUa Aktion markieren und die Eigenschaften parametrieren:

refIdmethodvaluestatic

Im Auswahldialog "SetTemperature" auswählenSetValueNumberstatic35

8)Projekt kompilieren, übertragen und in Visualisierung anzeigen und mit verschiedenen Benutzern testen

## Page 69

EREIGNISSE UND AKTIONEN69

Ergebnis:

Abbildung 69: Projektieren der OPC UA Aktion

Der Wert von "SetTemperature" wird nur dann gesetzt, wenn alle definierten Parameter (OPC UA Default

View) sowie die Berechtigungen für die eingeloggte Rolle/Benutzer erfüllt sind.

Abbildung 70: Visualisierung im Browser mit eingeloggtem Benutzer "UserService"

Jedem Widgets sollte bei seiner Erstellung immer ein eindeutiger Name gegeben werden, um es in einem

EventBinding zuverlässig finden zu können.

Aufgabe: Image-Widget bei Erreichen eines Wertes ein- und ausblenden

Ziel dieser Aufgabe ist es, beim Erreichen eines bestimmten Wertes des OPC UA Nodes "CurrentTemperature" (Ereig-

nis), ein Image auf dem Content der "MainPage" einzublenden (Aktion).

Um dieses Ziel zu erreichen, wird auf "ContentMainPage" ein Image eingefügt und dessen Sichtbarkeit auf "false" ge-

setzt. Anschließend wird in dem EventBinding das Ereignis beim Erreichen des Wertes sowie die Aktion für das Ein-

blenden des Images definiert.

1)"ContentMainPage" öffnen

2)Image-Widget einfügen und parametrieren:

## Page 70

70 ARBEITEN MIT MAPP VIEW TM611
Appearance \ image Appearance \ Behavior Common \ Name Layout\ Layout \
imageColor \ visible Position Size
Media/SymbolLib/Alarm/ rgba(255, 136, 0, 1) false ImageWarning 240; 580 40; 40
Alarm.svg
3) "ContentMainPage.eventbinding" öffnen
4) Aus dem Objektkatalog ein "opcUa.Event" einfügen
5) Das opcUa Ereignis markieren und die Eigenschaften ändern:
event refId
ValueChanged Im Auswahldialog "SetTemperature" auswählen
6) Aus dem Objektkatalog die "widgets.Action" auf das EventBinding ziehen
7) Auf Execute klicken und die Eigenschaft "Condition" ändern zu:
newValue >= 45
8) Die Widget Aktion markieren und die Eigenschaften ändern:
contentId widgetId method value static
ContentMainPage ImageWarning SetVisible static true
9) Aus dem Objektkatalog die "widgets.Action" auf das EventBinding ziehen
10)Auf Execute klicken und die Eigenschaft "Condition" ändern zu:
newValue < 45
11) Die Widget Aktion markieren und die Eigenschaften ändern:
contentId widgetId method value static
ContentMainPage ImageWarning SetVisible static false
12)Projekt kompilieren, übertragen und in Visualisierung anzeigen und mit verschiedenen Benutzern testen

## Page 71

EREIGNISSE UND AKTIONEN71

Ergebnis:

Abbildung 71: Eigenschaften für das Image-Widget

Abbildung 72: EventBinding Datei mit geöffnetem Binding mit einem OPC UA Ereignis und zwei Widget Aktionen

## Page 72

72ARBEITEN MIT MAPP VIEW TM611

Sobald "SetTemperature" größer oder gleich 45 ist, wird das Warndreieck eingeblendet. Sinkt der Wert

unter 45, so is das Image-Widget wieder unsichtbar.

Abbildung 73: Visualisierung im Browser mit eingeblendetem Image

Aufgabe: Automatischer Seitenwechsel nach Logout

Ziel dieser Aufgabe ist es beim Logout eines Benutzers automatisch zurück zu "MainPage" zu wechseln, wenn beim

Logoutvorgang "ServicePage" geöffnet war. Bis jetzt verblieb die Visualisierung nach einem Logout des Benutzers auf

"ServicePage".

Dazu wird ein Eventbinding auf das Klick-Ereignis des LogoutButton-Widgets erstellt und mit einem Operanden die

aktuelle Seite ausgelesen. Die Aktion "Navigate" wird nur ausgeführt, wenn die aktuelle Seite "ServicePage" war.

1)"ContentTop" öffnen

2)Auf LogoutButton-Widget klicken

3)Im Eigenschaftsfenster auf "Ereignisse" (Blitz-Icon) klicken

4)Auf das Feld "Click" klicken

Dadurch wird in der Configuration View eine neue EventBinding Datei erstellt

5)Das EventBinding aufklappen

6)Aus dem Objektkatalog eine "session.Operand" im Bereich "Operands" einfügen

7)Den Operanden markieren und die Eigenschaften ändern:

OperandNamerefIdmethoddatatype

CurrentPageclientInfo.currentPageIdGetValueANY_STRING

8)Aus dem Objektkatalog die "clientSystem.Action" auf das EventBinding ziehen

9)Auf Execute klicken und die Eigenschaft "Condition" ändern zu:

CurrentPage = "ServicePage"

10)Die Widget Aktion markieren und die Eigenschaften ändern:

methodpageIdstatic

NavigatestaticMainPage

11)Projekt kompilieren, übertragen und in Visualisierung anzeigen und mit verschiedenen Benutzern testen

## Page 73

EREIGNISSE UND AKTIONEN73

Ergebnis:

Abbildung 74: EventBinding zum automatischen Seitenwechsel von "ServicePage" zu "MainPage" nach Logout

## Page 74

74 ARBEITEN MIT MAPP VIEW TM611
13 Zusammenfassung
Das Trainingsmodul beschreibt die Nutzung von mapp View, einem Teil des mapp Technology Software-Pakets
von B&R. Es ermöglicht Automatisierungstechnikern, ohne tiefgehende Web-Technologie-Kenntnisse, Web-Visualisie-
rungsseiten zu erstellen.
Die Projektierung erfolgt in der B&R-Automatisierungssoftware Automation Studio, wobei die Visualisierungselemen-
te in der Logical View verwaltet und in der Configuration View zu Visualisierungen zusammengefügt werden.
Enthalten sind Informationen zu den folgenden Themen: die Authentifizierung über den mapp View Server, die Nut-
zung von Visualisierungsvorlagen, die Erstellung von Seiten und Navigation, Styling, Datenanbindung über OPC UA, die
Einbindung von Mediendateien, das Benutzer-Rollen-System, Lokalisierung von Texten und Einheiten sowie die Konfi-
guration von Ereignissen und Aktionen zur individuellen Anpassung der Visualisierung.

## Page 75

AUTOMATION ACADEMY75

Automation Academy

Ihr Wissensvorsprung

Die Automation Academy ist für die  unserer Kunden und eigenen Mitarbeiter verantwort-zielgerichtete Weiterbildung

lich. Erweitern Sie Ihre Kompetenzen auf dem Gebiet der Automatisierungstechnik und lernen Sie selbstständig effi-

mit B&R-Systemen zu realisieren.ziente Automatisierungslösungen

Entscheiden Sie selbst, mit welchem Sie lernen möchten!Lernkonzept

PräsenztrainingVirtuelles KlassenzimmerOnlinekurse

Ein erfahrener Trainer führt SieEin ortsunabhängiges Fernstudi-Sie erarbeiten sich Ihr Wissen

durch das Lernprogramm. Vor Ortum ergänzt das Lernangebot. Einselbstständig und bestimmen

am gewünschten B&R-Standort.Online-Tutor begleitet Sie virtu-Tempo und Inhalte selbst. Online-

Lernen individuell oder in kleinenell. Der Schwerpunkt liegt auf demkurse sind jederzeit verfügbar und

Lerngruppen.Selbststudium.unabhängig von Lernzeit und Lern-

ort.

Kontakt

Sie haben ein aktuelles Weiterbildungsanliegen? Sie interessieren sich für die Angebote der B&R Automation

Academy? Bei uns sind Sie genau richtig!

Hier erhalten Sie weitere Informationen:

https://www.br-automation.com/de/academy/

Viel Spaß bei Ihrem nächsten Training!

## Page 76

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V6.0.0.1 ©2025/01/08 by B&R, Alle Rechte vorbehalten.

Alle eingetragenen Warenzeichen sind Eigentum der jeweiligen Firma.

Technische Änderungen vorbehalten.