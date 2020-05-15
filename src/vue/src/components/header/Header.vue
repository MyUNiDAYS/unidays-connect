<template>
    <header id="header">
        <TopBar />
        <div class="middle-bar">
            <div class="container">
                <div class="middle-bar-inner">
                    <div class="logo">
                        <router-link to="/">
                            <img
                                src="@/assets/images/logo.png"
                                alt="Oellfare"
                            />
                        </router-link>
                    </div>
                </div>
            </div>
        </div>
        <div class="nav-bar">
            <div class="container">
                <div class="navigation">
                    <!-- <div class="element phone">
            <img src="@/assets/images/phone.png" alt="phone" />
            <div><span>Contact us to donate</span><br />(+123)987.654.32</div>
          </div> -->
                    <Navigation />
                    <!-- <div class="element login">
            <a href="#" class="power_button effect_1">
              <span class="button_value">Donate Now >></span>
            </a>
          </div> -->
                </div>
            </div>
        </div>
    </header>
</template>
<script>
import TopBar from "./TopBar";
import Navigation from "./Navigation";
export default {
    components: {
        TopBar,
        Navigation
    }
};
</script>
