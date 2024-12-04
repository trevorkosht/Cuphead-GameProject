using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Sprint0;

public class WinMenu : Menu
{
    private Rectangle winMenuBGDestRect, winMenuCupheadDestRect, winMenuCupheadSrcRect, winMenuBoardDestRect, winMenuResultsTextDestRect, winMenuResultsTextSrcRect, winMenuLineDestRect, star1DestRect, star2DestRect, winMenuCircleDestRect, winMenuCircleSrcRect, nextHitbox, nextBGRect;
    private Vector2 timeTextPosition, hpTextPosition, parryTextPosition, coinTextPosition, skillTextPosition, gradeTextPosition, nextTextPosition;
    private int updateCount, cupheadCurrFrameCount, resultsTextCurrFrameCount, circleCurrFrameCount, remainingPlayerHealth, parryCount, coinCount, gradePoint;
    private string grade, formattedTime;
    Texture2D star1, star2;
    private const int PARRY_GRADE_PERCENTAGE = 5;
    private const int COIN_GRADE_PERCENTAGE = 5;
    private const int HEALTH_GRADE_PERCENTAGE = 10;
    public WinMenu(Game1 game) : base(game){
        updateCount = cupheadCurrFrameCount = resultsTextCurrFrameCount = circleCurrFrameCount = 0;
    }

    public override void LoadContent(Texture2DStorage textureStorage)
    {
        remainingPlayerHealth = GOManager.Instance.Player.GetComponent<HealthComponent>().currentHealth / 100;
        parryCount = game.getPlayerStats()[0];
        coinCount = game.getPlayerStats()[1];
        grade = calculateLevelGrade();
        if(game.bossLevel) {
            grade = calculateBossGrade();
        }
        formattedTime = getFormattedGameTime();

        textures["WinScreenBackground"] = textureStorage.GetTexture("WinScreenBackground");
        textures["WinScreenBoard"] = textureStorage.GetTexture("WinScreenBoard");
        textures["WinScreenLine"] = textureStorage.GetTexture("WinScreenLine");
        textures["WinScreenStar"] = textureStorage.GetTexture("WinScreenStar");
        textures["WinScreenUnearnedStar"] = textureStorage.GetTexture("WinScreenUnearnedStar");
        textures["WinScreenCircle"] = textureStorage.GetTexture("WinScreenCircle");
        textures["WinScreenResultsText"] = textureStorage.GetTexture("WinScreenResultsText");
        textures["WinScreenCuphead"] = textureStorage.GetTexture("WinScreenCuphead");

        star1 = star2 = textures["WinScreenUnearnedStar"];
        calculateSkill();

        winMenuBGDestRect = new Rectangle(-200, -100, 1680, 900);

        winMenuCupheadDestRect = new Rectangle(100, 200, 475, 400);
        winMenuCupheadSrcRect = new Rectangle(0, 0, 450, 450);

        winMenuBoardDestRect = new Rectangle(550, 190, 575, 450);

        winMenuResultsTextDestRect = new Rectangle(185, 30, 900, 150);
        winMenuResultsTextSrcRect = new Rectangle(0, 0, 800, 150);

        winMenuLineDestRect = new Rectangle(650, 500, 370, 5);

        star1DestRect = new Rectangle(970, 450, 30, 30);
        star2DestRect = new Rectangle(1005, 450, 30, 30);

        winMenuCircleDestRect = new Rectangle(895, 500, 75, 75);
        winMenuCircleSrcRect = new Rectangle(0, 0, 90, 90);

        nextBGRect = nextHitbox = new Rectangle(775, 600, 120, 50);

        timeTextPosition = new Vector2(640, 260);
        hpTextPosition = new Vector2(642, 305);
        parryTextPosition = new Vector2(640, 350);
        coinTextPosition = new Vector2(640, 395);
        skillTextPosition = new Vector2(640, 440);
        gradeTextPosition = new Vector2(690, 515);
        nextTextPosition = new Vector2(783, 605);
    }

    public override void Update(GameTime gameTime)
    {
        game.paused = true;
        game.gameState = GameState.WinMenu;
        if(nextHitbox.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed) {
            game.paused = false;
        }

        if(updateCount % 5 == 0) {

            cupheadCurrFrameCount++;
            resultsTextCurrFrameCount++;
            circleCurrFrameCount++;

            if(cupheadCurrFrameCount == 12) {
                cupheadCurrFrameCount = 0;
            }
            if(resultsTextCurrFrameCount == 3) {
                resultsTextCurrFrameCount = 0;
            }
            if(circleCurrFrameCount == 12) {
                circleCurrFrameCount = 10;
            }
        }
        winMenuCupheadSrcRect = new Rectangle(450 * cupheadCurrFrameCount, 0, 450, 450);
        winMenuResultsTextSrcRect = new Rectangle(800 * resultsTextCurrFrameCount, 0, 800, 150);
        winMenuCircleSrcRect = new Rectangle(90 * circleCurrFrameCount, 0, 90, 90);

        updateCount++;
    }

    public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        spriteBatch.Draw(textures["WinScreenBackground"], winMenuBGDestRect, Color.White);
        
        spriteBatch.Draw(textures["WinScreenResultsText"], winMenuResultsTextDestRect, winMenuResultsTextSrcRect, Color.White);
        spriteBatch.Draw(textures["WinScreenCuphead"], winMenuCupheadDestRect, winMenuCupheadSrcRect, Color.White);

        spriteBatch.Draw(textures["WinScreenBoard"], winMenuBoardDestRect, Color.White);
        spriteBatch.DrawString(font, "TIME.................." + formattedTime, timeTextPosition, Color.White);
        spriteBatch.DrawString(font, "HP BONUS..........." + remainingPlayerHealth + "/3", hpTextPosition, Color.White);

        if(!game.bossLevel) {
            spriteBatch.DrawString(font, "PARRY.................." + parryCount + "/4", parryTextPosition, Color.White);
            spriteBatch.DrawString(font, "GOLD COIN.........." + coinCount + "/4", coinTextPosition, Color.White);
        }

        spriteBatch.DrawString(font, "SKILL LEVEL........    ", skillTextPosition, Color.White);
        spriteBatch.Draw(star1, star1DestRect, Color.White);
        spriteBatch.Draw(star2, star2DestRect, Color.White);

        spriteBatch.Draw(textures["WinScreenLine"], winMenuLineDestRect, Color.White);
        spriteBatch.DrawString(font, "GRADE....... " + grade, gradeTextPosition, Color.Goldenrod);
        spriteBatch.Draw(textures["WinScreenCircle"], winMenuCircleDestRect, winMenuCircleSrcRect ,Color.White);

        if(!game.bossLevel) {
            spriteBatch.FillRectangle(nextBGRect, Color.DarkGray);
            spriteBatch.DrawRectangle(nextHitbox, Color.Black, 3f);
            spriteBatch.DrawString(font, "NEXT", nextTextPosition, Color.Black);
        }
    }

    private string getFormattedGameTime() {
        
        string minutesString = "0";
        string secondsString = "0";

        int minutes = totalGametimeSeconds / 60;
        int seconds = totalGametimeSeconds % 60;

        if(minutes >= 10) {
            minutesString = minutes.ToString();
        } else {
            minutesString += minutes.ToString();
        }

        if(seconds >= 10) {
            secondsString = seconds.ToString();
        } else {
            secondsString += seconds.ToString();
        }

        return minutesString + ":" + secondsString;
    }

    private void calculateSkill() {
        if(gradePoint >= 3) {
            star1 = textures["WinScreenStar"];
        }
        if(gradePoint >= 9) {
            star1 = star2 = textures["WinScreenStar"];
        }
    }

    private string calculateLevelGrade() {
        int percentage = (HEALTH_GRADE_PERCENTAGE * remainingPlayerHealth) + (PARRY_GRADE_PERCENTAGE * parryCount) + (COIN_GRADE_PERCENTAGE * coinCount); 

        if(totalGametimeSeconds <= 120) {
            percentage += 30;
        } else if (totalGametimeSeconds <= 180) {
            percentage += 20;
        } else if (totalGametimeSeconds <= 240) {
            percentage += 10;
        }

        gradePoint = percentage / 8;

        switch (gradePoint){
            case 12:
                return "S";
            case 11:
                return "A+";
            case 10:
                return "A";
            case 9:
                return "A-";
            case 8:
                return "B+";
            case 7:
                return "B";
            case 6:
                return "B-";
            case 5:
                return "C+";
            case 4:
                return "C";
            case 3:
                return "C-";
            case 2:
                return "D+";
            case 1:
                return "D";
            case 0:
                return "D-";
            default:
                return "N/A";
        }
    }

    private string calculateBossGrade() {
        int percentage = 2 * HEALTH_GRADE_PERCENTAGE * remainingPlayerHealth;

        if(totalGametimeSeconds <= 180) {
            percentage += 40;
        } else if (totalGametimeSeconds <= 180) {
            percentage += 30;
        } else if (totalGametimeSeconds <= 240) {
            percentage += 20;
        }

        gradePoint = percentage / 8;

        switch (gradePoint){
            case 12:
                return "S";
            case 11:
                return "A+";
            case 10:
                return "A";
            case 9:
                return "A-";
            case 8:
                return "B+";
            case 7:
                return "B";
            case 6:
                return "B-";
            case 5:
                return "C+";
            case 4:
                return "C";
            case 3:
                return "C-";
            case 2:
                return "D+";
            case 1:
                return "D";
            case 0:
                return "D-";
            default:
                return "N/A";
        }
    }
}