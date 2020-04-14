<template>
  <div class="top-bar">
    <div class="container">
      <div class="top-bar-inner">
        <div class="top-element">
          <div class="top-left-elements">
            <div class="element schedule">
              <img src="@/assets/images/clock.png" alt="schedule" />
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
</template>
<script>
import SocialLinks from "./SocialLinks";
export default {
  components: {
    SocialLinks
  }
};
</script>
