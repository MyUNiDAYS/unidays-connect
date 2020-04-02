<template>
  <header id="header">
    <div class="top-bar">
      <div class="container">
        <div class="top-bar-inner">
          <div class="top-element">
            <div class="top-left-elements">
              <div class="element schedule">
                <img src="images/clock.png" alt="schedule" />
                <div>Mon-Sat, 9.00am-9.00pm</div>
              </div>
            </div>
            <div class="top-right-elements">
              <SocialLinks />
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="middle-bar">
      <div class="container">
        <div class="middle-bar-inner">
          <div class="logo">
            <a href="index.html">
              <img src="images/logo.png" alt="Oellfare" />
            </a>
          </div>
        </div>
      </div>
    </div>
    <div class="nav-bar">
      <div class="container">
        <div class="navigation">
          <div class="element phone">
            <img src="images/phone.png" alt="phone" />
            <div><span>Contact us to donate</span><br />(+123)987.654.32</div>
          </div>
          <Navigation />
          <div class="element login">
            <a href="#" class="power_button effect_1">
              <span class="button_value">Donate Now >></span>
            </a>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>
<script>
import SocialLinks from "@/components/header/SocialLinks";
import Navigation from "@/components/header/Navigation";
export default {
  components: {
    SocialLinks,
    Navigation
  }
};
</script>
