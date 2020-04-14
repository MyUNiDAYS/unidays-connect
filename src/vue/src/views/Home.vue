<template>
  <main>
    <div class="main-wrapper">
      <HomeHero />
      <HomeEvents />
      <NewsLetterSignup />
    </div>
  </main>
</template>

<script>
import HomeHero from "@/components/home/HomeHero";
import HomeEvents from "@/components/home/HomeEvents";
import NewsLetterSignup from "@/components/home/NewsLetterSignup";
export default {
  name: "Home",
  components: {
    HomeHero,
    HomeEvents,
    NewsLetterSignup
  }
};
</script>
